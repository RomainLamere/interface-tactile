const v3 = require('node-hue-api').v3;
const LightState = v3.lightStates.LightState;
const { StringDecoder } = require('string_decoder'); // To decode string for JSON

const USERNAME = '6b9DYoVL9IuxX72XToSFmR1JpUQCzj98DpbHHFXS'
// The name of the light we wish to retrieve by name
, LIGHT_ID = 3;

function log(message){
  console.log(`SetLights | ${message}`);
}

function hexToRgb(hex) {
  var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
  if(result){
      var r= parseInt(result[1], 16);
      var g= parseInt(result[2], 16);
      var b= parseInt(result[3], 16);
	  return '{"r": '+r+', "g": '+g+', "b":'+b+'}';
  } 
  return null;
}

function setLights(rgbTxt) {
	var hexRGB = hexToRgb(rgbTxt);
	log(`Set Lights : ${hexRGB}`);
	setLight(1, hexRGB);
	setLight(2, hexRGB);
	setLight(3, hexRGB);
};

function setLight(light_id, hexRGB) {
  let rgbJSON = JSON.parse(hexRGB);
  v3.discovery.nupnpSearch()
  .then(searchResults => {
    const host = searchResults[0].ipaddress;
    return v3.api.createLocal(host).connect(USERNAME);
  })
  .then(api => {
    // Using a LightState object to build the desired state
    const state = new LightState()
    .on()
    .ct(500) // Temp between 153 (6500K) and 500 (2000K)
    .brightness(5) // Brightness 0-100
    .rgb(rgbJSON.r,rgbJSON.g, rgbJSON.b) // 0-255
    //.effect('none')// Cycle colors
    ;

    return api.lights.setLightState(light_id, state);
  })
  .then(result => {
    console.log(`Light state change was successful? ${result}`);
  });
};


module.exports = {
  setLights: setLights
};
