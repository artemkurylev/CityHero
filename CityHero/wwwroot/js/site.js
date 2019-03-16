// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


const apihost = "";
const headers = new Headers({
  'Content-Type' : 'applicaton/json',
  Accept : 'application/json'
});

function objectToUrl(obj){
  let url = '';
  for ( let key in obj){
    url += key + '=';
    let val = obj[key];
    if( Array.isArray(val)){
      url += '[' + val.join(',') + ']'
    } 
    else if (typeof url == 'object'){
      url += '{' + objectToUrl(val) + '}'
    }
    else {
      url += val;
    }
    url += '&'
  }
  url = url.slice(0,-1);
  return url;
}

function makeRequest({type = "get", request = "", body = undefined}){
  type = type.toLowerCase();
  
  let requestUrl = apihost + '/api/' +
    ( request.length > 0 && request[0] === '/' ? request.slice(1) : request) ;
  
  if( type != 'get'&& body != undefined && typeof body != 'string'){
    body = JSON.stringify( body);
  }
  else if ( type == 'get' && body != undefined && typeof body == 'object') {
    const getParams = objectToUrl( body);
    requestUrl += '?' + getParams;
  }

  return fetch( requestUrl, {
    method: type,
    mode: 'cors',
    headers,
    body: type == 'get' ? undefined : body
  }).then( 
	resp => resp.json()
	);
}

const controllers = [
  'Users',
  'Places',
]

API = {

}

for ( controller of controllers) {
  API[controller] = {}
}


for ( controller of controllers) {
  API[controller].get = (id) => makeRequest({type:"get", request: controller + '/' + id})
  API[controller].getAll = () => makeRequest({type:"get", request: controller + '/'})
  API[controller].create = (val) => makeRequest({type:"post", request: controller + '/' + id})
  API[controller].remove = (id) => makeRequest({type:"delete", request: controller + '/' + id })
  API[controller].update = (id, val) => makeRequest({type:"put", request: controller + '/'+ id, body : val })
}


function fillServerInfo( map, user, places ) {
	let userPlaceIds = [];

	function fillUser( map, user) {
		let markers = DS.featureGroup();
		for ( let vp of user.visitedPlaces) {
			
			userPlaceIds.push( vp.place.id);
			
			DG.marker([vp.place.coordY, vp.place.coordX ], {
				//icon: myIcon
			}).addTo(markers);
		}
		markers.addTo(map);
	}

	function fillPlaces( map, places, excludePoints){
		let polygons = DG.featureGroup();
		let lines= DG.featureGroup();
		
		let markers = DS.featureGroup();
		
		for ( let place of places.slice(0,)){
			if( excludePoints.indexOf( place.id) == -1 ){
				myIcon = DG.icon({
					iconUrl: '../images/marker-red.png',
					iconSize: [48, 48]
				});

				DG.marker([place.coordY, place.coordX ], {
					//icon: myIcon
				}).addTo(markers);
				
				/*
				const latlngs = [];
				for ( let pa of place.placeArea){
					//latlngs.push( [ pa.point.coordX, pa.point.coordY]);
					latlngs.push( [ pa.point.coordY, pa.point.coordX]);
				}
				if( latlngs.length > 2)
					DG.polygon(latlngs, {color: "black"}).addTo(polygons);
				else
					DG.polyline( latlngs, {color:"gray"}).addTo(lines)
				*/
			}
		}
		markers.addTo(map);
		
		//polygons.addTo(map);
		//lines.addTo(map);
		
		
		// Подстройка границ карты
		map.fitBounds(markers.getBounds());

	}

	fillUser( map, user);
	fillPlaces( map, places, userPlaceIds);
}
// Write your JavaScript code.
var map;

let userId= -1;

DG.then(function () {
	map = DG.map('map', {
		center: [48.72, 44.52],
		zoom: 15
	});

	var marker = DG.marker([48.72, 44.52]);

	marker.addTo(map).bindPopup('Вы кликнули по мне!');

	marker.on('click', function() {
		$('#popupMenu').addClass('show');
	});

	marker.on('popupclose', function() {
		$('#popupMenu').removeClass('show');
	});

	$('#popupClose').on('click', function() {
		marker.closePopup();
	})
	
	
	API.Places.getAll()
	.then( 
		pls => API.Users.get(userId).then(
			user => fillServerInfo( map, user, pls)
		)
	)
});












