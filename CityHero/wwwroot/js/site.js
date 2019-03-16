﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var map;

DG.then(function () {
	map = DG.map('map', {
		center: [48.72, 44.52],
		zoom: 15
	});

	var marker = DG.marker([48.72, 44.52]);

	marker.addTo(map).bindPopup('Вы кликнули по мне!');

	marker.on('click', function() {
		$('#popupPlace').addClass('show');
	});

	marker.on('popupclose', function() {
		$('#popupPlace').removeClass('show');
	});

	$('#popupPlaceClose').on('click', function() {
		marker.closePopup();
	});
	$('#popupQuestionClose').on('click', function() {
		$('#popupQuestion').removeClass('show');
	});

	$('#btn-question').on('click', function() {
		$('#popupQuestion').addClass('show');
	});
});

function setProgressValue(value) {
	var bar = $('.progress-bar');
	bar.attr('aria-valuenow', value);
	bar.css('width', value + '%');
	bar.text(value + '%');
}