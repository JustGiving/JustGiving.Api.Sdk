'use strict';

var Client = require('../dist/justgiving-apiclient.js').ApiClient;

require('es6-promise').polyfill();
global.fetch = require('node-fetch');

var client = new Client('https://api-sandbox.justgiving.com', 'c1072ac8');

client.getCountries()
  .then(function(response) {console.log(response);})
  .catch(function(error) {console.error(error);});
