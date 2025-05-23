
var map = L.map('map', {
    crs: L.CRS.Simple,
    minZoom: -2,
    maxZoom: 3
});

//Image
axios.get("https://localhost:7012/api/Authentication/Region")
.then(res=>{
    mapImage = res.data.picture
    
    mapWidth = res.data.width
    mapHeight = res.data.height
    console.log(res.data)   
    
    var bounds = [[0,0], [-mapHeight, mapWidth]];

    var image = L.imageOverlay(`data:image/png;base64,${mapImage}`, bounds).addTo(map);

    map.fitBounds(bounds);
})

//Tag
axios.get("https://localhost:7012/api/Authentication/Tag")
.then(res=>{
    altitude = res.data.altitude
    console.log(res.data)  

    res.data.forEach(element => {
        var circle = L.circle([-element.latitude, element.longitude], {
            color: 'red',
            fillColor: '#f03',
            fillOpacity: 0.5,
            radius: 8
        }).addTo(map);
        circle.bindPopup(element.code.toString());
    });
})

var popup = L.popup();

function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent("Coordinate " + e.latlng.toString())
        .openOn(map);
}


map.on('click', onMapClick);

var drawnItems = new L.FeatureGroup();
    map.addLayer(drawnItems);
    var drawControl = new L.Control.Draw({
        draw: {
            polygon: false,
            marker: false,
            rectangle: false,
            circle: false
        },
        edit: {
            featureGroup: drawnItems,
            edit: false
        }
    });
    
    map.addControl(drawControl);


//Konumlar arası koordinatlar

var polylineLatLng = [];

function polyLine(){
    map.on('draw:created', function (e) {
        var type = e.layerType,
        layer = e.layer;

        var i = 0;
        layer.getLatLngs().forEach(element => {
            if ((type === 'polyline') && (i % 2 == 0)) {
                polylineLatLng.push(element);             
            }
            i++;
        }); 
        console.log(polylineLatLng) 

        axios.post("https://localhost:7012/api/Coordinate", polylineLatLng)
        .then(response =>{
            console.log(response.data);
            response.data.forEach(element =>{
            var marker = L.marker([element.lat, element.lng]).addTo(map);
            })       
        })

        axios.post("https://localhost:7012/api/Coordinate/Distance", polylineLatLng)
        .then(response =>{
            console.log(response.data);    
        })

        axios.post("https://localhost:7012/api/Coordinate/MessageType", polylineLatLng)
        .then(response =>{
            console.log(response.data);    
        })
    }); 
}

var result = polyLine();

function SendServer(){
    var hostname = "localhost";
    var port = 11050;
    axios.post("https://localhost:7012/api/Socket/SocketClient?hostname=" + hostname + "&port=" + port, polylineLatLng)
    // .then(response => {
    //     console.log(response.data);
    // })
}
