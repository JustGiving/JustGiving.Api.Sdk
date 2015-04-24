[![Written in ES2015](https://img.shields.io/badge/Written%20in-ES2015-lightgrey.svg)](http://wiki.ecmascript.org/doku.php?id=harmony:specification_drafts)
[![Transpiled with Babel](https://img.shields.io/badge/Transpiled%20with-Babel-yellow.svg)](https://babeljs.io)
[![Built with Gulp](https://img.shields.io/badge/Built%20with-Gulp-orange.svg)](http://gulpjs.com)
[![Tested with Mocha](https://img.shields.io/badge/Tested%20with-Mocha-green.svg)](http://http://mochajs.org/)
[![Linted with ESLint](https://img.shields.io/badge/Linted%20with-ESLint-blue.svg)](http://eslint.org/)

JavaScript SDK
==============

Modern JG client using the Fetch API and promises.
Works in IE10 and above using polyfills.

7KB minified.

Getting Started
---------------

### Bower

Stay tuned

### NPM

Stay tuned

### Download the Source

```bash
git clone https://github.com/JustGiving/JustGiving.Api.Sdk.git
cd ./JustGiving.Api.Sdk/JavaScript
```

### Building from source

To get the latest dependencies, build and run tests:

```bash
npm update
bower update
gulp
```

Samples
-------

Browser-based examples - ```method-samples.html``` and ```chained.knockout.html```.
Node example - ```server.node.js```.

To run the examples, first create a credentials.js file in the examples directory:

```javascript
var credentials = {
  appId: '{your app ID}',
  basicAuthToken: 'Basic {your base64-encoded username/password}'
};
```

Then run gulp which will run a local webserver on port 8000.

Roadmap
-------

- [ ] Full API coverage
- [ ] Publish to Bower, Jam
- [ ] Publish to npm
- [ ] Test with Testling
- [ ] Browser examples in gh-pages branch
- [ ] More examples, including Rx
- [ ] JSDoc

Compatibility
-------------

The JavaScript API works natively with modern browsers (eg Chrome 43) and using polyfills back to IE10.

License
-------

Copyright (c) 2010 Giving.com, trading as JustGiving. Licensed under the Apache License (v2.0).
