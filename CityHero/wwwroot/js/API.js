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
  
  let requestUrl = apihost + 
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
  });
}

const controllers = [
  'Users',
  'Places',
]

const API = {

}

for ( controller of controllers) {
  API[controller] = {}
}


for ( controller of controllers) {
  API[controller].get = (id) => makeRequest({type="get", request = controller + '/' + id})
  API[controller].getAll = () => makeRequest({type="get", request = controller + '/'})
  API[controller].create = (val) => makeRequest({type="put", request = controller + '/' + id})
  API[controller].remove = (id) => makeRequest({type="delete", request = controller + '/' + id })
  API[controller].update = (id, val) => makeRequest({type="post", request = controller + '/'+ id, body = val })
}


fillPlaces( map, places){
	polygons = DG.featureGroup();
	
1	for( let place in places){
		const latlngs = [];
		for ( pa in places.placeArea){
			latlngs.push( [ p.coordX, p.coordY]);
		}
		DG.polygon(latlngs, {color: "black"}).addTo(polygons);
	}
	
	polygons.addTo(map);
	
	
	// Подстройка границ карты
	map.fitBounds(polygons.getBounds());
}