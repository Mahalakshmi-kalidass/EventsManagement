$(document).ready(function () {
    function getCoords(location) {

        var url = 'https://nominatim.openstreetmap.org/search?format=json&q=' + location;
        $.ajax({
            url: url,
            dataType: 'json',
            success: function (data) {
                console.log(data);
                if (data.length > 0) {
                    var latitude = data[0].lat;
                    var longitude = data[0].lon;
                    console.log('Latitude:', latitude);
                    console.log('Longitude:', longitude);
                    // Creating a Marker
                    var markerOptions = {
                        title: location,
                        clickable: true,
                        draggable: true
                    }

                    // Creating a marker
                    var marker = L.marker([latitude, longitude], markerOptions);

                    // Adding marker to the map
                    marker.addTo(map);

                } else {
                    console.log('Location not found.');
                }
            },
            error: function () {
                console.log('Error fetching coordinates.');
            }
        });
    }
    function getEventLocations() {
        $.ajax({
            url: 'https://localhost:44354/api/Location/GetAllLocation',
            type: 'GET',
            success: function (data) {
                console.log(data);
                $.each(data, function (index, obj) {
                    getCoords(obj.locationName);
                })
            },
            error: function (error) {
                console.log(error);
            }

        })
    }
    switch (page) {
        case 'AdminHome':
            getEventLocations();

            // Creating a map object
            var map = L.map('map').setView([0, 0], 2); // args are center[lat, lon] and zoom

            // Creating a Layer object
            var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { noWrap: true });

            // Adding layer to the map
            map.addLayer(layer);

            break;
    }
    

    
    
  
   
});