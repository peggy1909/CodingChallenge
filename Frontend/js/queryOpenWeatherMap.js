const useOpenWeatherMap = false;

let baseUrl, urlWeather, urlImg, urlLocation, appId;

if(useOpenWeatherMap){
  baseUrl = "https://api.openweathermap.org";
  urlWeather = "/data/2.5/weather";
  urlImg = "http://openweathermap.org/img/wn/";
  urlLocation = "https://api.openweathermap.org/geo/1.0/reverse"
  appId = ""
}

else{
  baseUrl = "http://localhost:5293";
  urlWeather = "/data/weather";
  urlImg = "http://openweathermap.org/img/wn/";
  urlLocation = "https://api.openweathermap.org/geo/1.0/reverse"
  appId = ""
}


$(document).ready(function(){

  //Wetter basierend auf aktueller Location
  if(navigator.geolocation){
    navigator.geolocation.getCurrentPosition(function(position){
      const latitude = position.coords.latitude;
      const longitude = position.coords.longitude;

      getCityName(latitude, longitude, function(cityName) {
        getWeatherByCity(cityName);
      });

    })
  }


  //Vorschlag der Stadt
  $('#inputCity').focus(function() {
    if(navigator.geolocation){
      navigator.geolocation.getCurrentPosition(function(position){
        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;

        getCityName(latitude, longitude, function(cityName) {
          $('#inputCity').val(cityName);
        });
      })
    }
  });


  //Enter für Suche
  $('#inputCity').keypress(function(e) {
    if (e.which === 13) {
      $('#buttonCity').click();
    }
  });


  //Button "Stadt suchen"
  $("#buttonCity").click(function(){
    
    const cityName = $('#inputCity').val();
    if (cityName.trim() !== '') {
      getWeatherByCity(cityName);
    } else {
      alert('Please enter a city name.');
    }

  });
      
});

//Anzeigen der Werte
function setValues(result){
  $("#city").text(result.name + ", " + result.sys.country)

    $("#temperature").text(result.main.temp + " °C")
    $("#pressure").text(result.main.pressure + " hPa")
    $("#humidity").text(result.main.humidity + "%")
    $("#temperatureMin").text(result.main.temp_min + " °C")
    $("#temperatureMax").text(result.main.temp_max + " °C")
    $("#windSpeed").text(result.wind.speed + " m/s")
    $("#windDirection").text(result.wind.deg + " °")

    $("#cloudCoverCondition").text(result.weather[0].description)
    
    var iconCode = result.weather[0].icon;
    const iconUrl = `${urlImg}${iconCode}@2x.png`;

    $("#weatherIcon").attr("src", iconUrl);
}

//Wetterabfrage
function getWeatherByCity(city){
  const url = `${baseUrl}${urlWeather}?q=${city}&appid=${appId}&units=metric&lang=de`

  $.getJSON(url,function(result){

    setValues(result)
  });
}

//Umwandlung von Koordinaten in Stadtname
function getCityName(latitude, longitude, callback){
  const url = `${urlLocation}?lat=${latitude}&lon=${longitude}&appid=${appId}`;

  $.getJSON(url,function(result){
    callback(result[0].name);
  });
}