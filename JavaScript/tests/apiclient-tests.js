'use strict';

var should = require('should'),
    sinon = require('sinon'),
    JG = require('../dist/justgiving-apiclient.js').ApiClient;

require('es6-promise').polyfill();

global.fetch = function() {};

describe('Given JG class', function() {
  describe('when constructor called without URL', function() {
    it('should throw error', function() {
      (function() {new JG()}).should.throw('URL is required');
    });
  });

  describe('when constructor called correctly', function() {
    it('should create new instance', function() {
      new JG('https://api-sandbox.justgiving.com');
    });
  });
});

describe('Given JG client instance', function() {
  var mock, fetchMock, target = new JG('https://baseurl', '0123456789', 'Basic ABC');
  var headMatcher = sinon.match(function(value) {return value.method === 'HEAD';}, 'HEAD method');
  var deleteMatcher = sinon.match(function(value) {return value.method === 'DELETE';}, 'DELETE method');
  var putMatcher = sinon.match(function(value) {return value.method === 'PUT';}, 'PUT method');
  var postMatcher = sinon.match(function(value) {return value.method === 'POST';}, 'POST method');

  beforeEach(function(){
    mock = sinon.mock(global);
    fetchMock = mock.expects("fetch")
                    .returns(Promise.resolve());
  });

  afterEach(function() {
    mock.restore();
  });

  it('should pass appID header', function() {
    var hasApiHeader = sinon.match(function(value) {return value.headers['x-app-id'] === '0123456789';}, 'appID');

    fetchMock
      .withArgs(sinon.match.any, hasApiHeader)
      .once();

    target.getCountries();
    fetchMock.verify();
  });

  it('should pass authorization header', function() {
    var hasAuthorizationHeader = sinon.match(function(value) {return value.headers['Authorization'] === 'Basic ABC';}, 'authorization');

    fetchMock
      .withArgs(sinon.match.any, hasAuthorizationHeader)
      .once();

    target.getCountries();
    fetchMock.verify();
  });

  it('should request JSON content', function() {
    var hasAcceptHeader = sinon.match(function(value) {return value.headers['Accept'] === 'application/json';}, 'acceptHeader');

    fetchMock
      .withArgs(sinon.match.any, hasAcceptHeader)
      .once();

    target.getCountries();
    fetchMock.verify();
  });

  it('should make successful getCountries request', function() {
    fetchMock
      .withArgs('https://baseurl/v1/countries')
      .once();

    target.getCountries();
    mock.verify();
  });

  describe('when checkAccountAvailability with valid local-part smtp address', function() {
    it('should pass the email address correctly', function() {
      fetchMock
        .withArgs('https://baseurl/v1/account/!%23%24%25%26\'()*%2B%2C%3B%3D%3A.%40example.org')
        .once();

      target.checkAccountAvailability('!#$%&\'()*+,;=:.@example.org');
      mock.verify();
    });
  });

  describe('when checkAccountAvailability with GMail account alias', function() {
    it('should pass the email address correctly', function() {
      fetchMock
        .withArgs('https://baseurl/v1/account/test%2Baccount%40gmail.com')
        .once();

      target.checkAccountAvailability('test+account@gmail.com');
      mock.verify();
    });
  });

  describe('when validateAccount', function() {
    var payloadMatcher = sinon.match(function(value) {return value.body === '{"email":"myAwesomeEmail@something.com","password":"myPassword"}';}, 'body');

    it('should post account details', function() {
      fetchMock
        .withArgs('https://baseurl/v1/account/validate', postMatcher.and(payloadMatcher))
        .once();

      target.validateAccount('myAwesomeEmail@something.com', 'myPassword');
      mock.verify();
    });
  });

  describe('when getAccountRating', function() {
    it('should pass pagination parameters', function() {
      fetchMock
        .withArgs(sinon.match('pageSize=10').and(sinon.match('page=3')))
        .once();

      target.getAccountRating(10, 3);
      mock.verify();
    });
    it('should omit undefined pagination parameters', function() {
      fetchMock
        .withArgs('https://baseurl/v1/account/rating?pageSize=10&')
        .once();

      target.getAccountRating(10);
      mock.verify();
    });
  });

  describe('when getCharity', function() {
    it('should pass charityID', function() {
      fetchMock
        .withArgs('https://baseurl/v1/charity/42')
        .once();

      target.getCharity(42);
      mock.verify();
    });
  });

  describe('when getDonation', function() {
    it('should pass donationID', function() {
      fetchMock
        .withArgs('https://baseurl/v1/donation/42')
        .once();

      target.getDonation(42);
      mock.verify();
    });
  });

  describe('when getDonationsForUser', function() {
    it('should pass charityId', function() {
      fetchMock
        .withArgs(sinon.match('charityId=50'))
        .withArgs('https://baseurl/v1/account/donations?pageSize=10&pageNum=1&charityId=50&')
        .once();

      target.getDonationsForUser(10, 1, 50);
      mock.verify();
    });
    it('should pass pagination parameters', function() {
      fetchMock
        .withArgs(sinon.match('pageSize=1').and(sinon.match('pageNum=1')))
        .once();

      target.getDonationsForUser(10, 1, 50);
      mock.verify();
    });
    it('should omit undefined pagination parameters', function() {
      fetchMock
        .withArgs('https://baseurl/v1/account/donations?charityId=50&')
        .once();

      target.getDonationsForUser(undefined, undefined, 50);
      mock.verify();
    });
  });

  describe('when getDonationByReference', function() {
    it('should pass reference', function() {
      fetchMock
        .withArgs('https://baseurl/v1/donation/ref/reference')
        .once();

      target.getDonationByReference('reference');
      mock.verify();
    });
  });

  describe('when getDonationStatus', function() {
    it('should pass donationID', function() {
      fetchMock
        .withArgs('https://baseurl/v1/donation/42/status')
        .once();

      target.getDonationStatus(42);
      mock.verify();
    });
  });

  describe('when getEvent', function() {
    it('should pass eventID', function() {
      fetchMock
        .withArgs('https://baseurl/v1/event/42')
        .once();

      target.getEvent(42);
      mock.verify();
    });
  });

  describe('when registerEvent', function() {
    it('should post event details', function() {
      fetchMock
        .withArgs('https://baseurl/v1/event', postMatcher)
        .once();

      target.registerEvent({});
      mock.verify();
    });
  });

  describe('when getEventPages without specifying page', function() {
    it('should pass eventID', function() {
      fetchMock
        .withArgs('https://baseurl/v1/event/42/pages?')
        .once();

      target.getEventPages(42);
      mock.verify();
    });
  });

  describe('when getEventPages with page details', function() {
    it('should pass pageSize and pageNum', function() {
      fetchMock
        .withArgs(sinon.match('pageSize=10').and(sinon.match('page=3')))
        .once();

      target.getEventPages(42, 10, 3);
      mock.verify();
    });
  });

  describe('when getProject', function() {
    it('should pass projectID', function() {
      fetchMock
        .withArgs('https://baseurl/v1/project/global/42')
        .once();

      target.getProject(42);
      mock.verify();
    });
  });

  describe('when getFundraisingPage', function() {
    it('should pass short name', function() {
      fetchMock
        .withArgs('https://baseurl/v1/fundraising/pages/my-short-name')
        .once();

      target.getFundraisingPage('my-short-name');
      mock.verify();
    });
  });

  describe('when suggestPageShortName', function() {
    it('should pass short name', function() {
      fetchMock
        .withArgs('https://baseurl/v1/fundraising/pages/suggest?preferredName=my-short-name')
        .once();

      target.suggestPageShortName('my-short-name');
      mock.verify();
    });
  });

  describe('when checkPageExists', function() {
    it('should pass short name', function() {
      fetchMock
      .withArgs('https://baseurl/v1/fundraising/pages/my-short-name', headMatcher)
      .once();

      target.checkPageExists('my-short-name');
      mock.verify();
    });
  });

  describe('when cancelFundraisingPage', function(){
    it('should pass short name', function() {
      fetchMock
      .withArgs('https://baseurl/v1/fundraising/pages/my-short-name', deleteMatcher)
      .once();

      target.cancelFundraisingPage('my-short-name');
      mock.verify();
    });
  });

  describe('when addFundraisingPageImage', function(){
    var payloadMatcher = sinon.match(function(value) {return value.body === '{"caption":"caption","isDefault":false,"url":"https://baseurl/image"}';}, 'body');

    it('should pass correct parameters', function() {
      fetchMock
        .withArgs('https://baseurl/v1/fundraising/pages/my-short-name/images', putMatcher.and(payloadMatcher))
        .once();

      target.addFundraisingPageImage('my-short-name', 'caption', 'https://baseurl/image', false);
      mock.verify();
    });

    it('should default isDefault parameter', function() {
      fetchMock
        .withArgs('https://baseurl/v1/fundraising/pages/my-short-name/images', payloadMatcher)
        .once();

      target.addFundraisingPageImage('my-short-name', 'caption', 'https://baseurl/image');
      mock.verify();
    });
  });

  describe('when checkTeamExists', function() {
    it('should pass short name', function() {
      fetchMock
        .withArgs('https://baseurl/v1/team/short-name', headMatcher)
        .once();

      target.checkTeamExists('short-name');
      mock.verify();
    });
  });

  describe('when joinTeam', function() {
    it('should pass team and page short names', function() {
      fetchMock
        .withArgs('https://baseurl/v1/team/join/short-name', putMatcher)
        .once();

      target.joinTeam('short-name', 'page-short-name');
      mock.verify();
    });
  });

  describe('when searchCharities', function() {
    it('should pass pagination parameters', function() {
      fetchMock
        .withArgs(sinon.match('pageSize=10').and(sinon.match('page=3')))
        .once();

      target.searchCharities('searchTerm', 50, [1], 3, 10);
      mock.verify();
    });
    it('should omit undefined pagination parameters', function() {
      fetchMock
        .withArgs('https://baseurl/v1/charity/search?q=searchTerm&categoryId=1&charityId=50&')
        .once();

      target.searchCharities('searchTerm', 50, [1]);
      mock.verify();
    });
    it('should append all category IDs', function() {
      fetchMock
        .withArgs(sinon.match('categoryId=1').and(sinon.match('categoryId=2')))
        .once();

      target.searchCharities('searchTerm', 50, [1, 2]);
      mock.verify();
    });
    it('should append all charity IDs', function() {
      fetchMock
        .withArgs(sinon.match('charityId=50').and(sinon.match('charityId=51')))
        .once();

      target.searchCharities('searchTerm', [50, 51], 1);
      mock.verify();
    });
    it('should append single category ID', function() {
      fetchMock
        .withArgs(sinon.match('categoryId=1'))
        .once();

      target.searchCharities('searchTerm', 50, 1);
      mock.verify();
    });
    it('should append single charity ID', function() {
      fetchMock
        .withArgs(sinon.match('charityId=50'))
        .once();

      target.searchCharities('searchTerm', 50, 1);
      mock.verify();
    });
  });

  describe('when searchEvents', function() {
    it('should pass pagination parameters', function() {
      fetchMock
      .withArgs(sinon.match('pageSize=10').and(sinon.match('page=3')))
      .once();

      target.searchEvents('searchTerm', 3, 10);
      mock.verify();
    });
    it('should omit undefined pagination parameters', function() {
      fetchMock
      .withArgs('https://baseurl/v1/event/search?q=searchTerm&')
      .once();

      target.searchEvents('searchTerm');
      mock.verify();
    });
  });

  describe('when oneSearch', function() {
    it('should pass pagination parameters', function() {
      fetchMock
      .withArgs(sinon.match('limit=3').and(sinon.match('offset=10')))
      .once();

      target.oneSearch('searchTerm', true, 'index', 3, 10);
      mock.verify();
    });
    it('should omit undefined pagination parameters', function() {
      fetchMock
      .withArgs('https://baseurl/v1/onesearch?q=searchTerm&g=true&i=index&country=GB')
      .once();

      target.oneSearch('searchTerm', true, 'index', undefined, undefined, 'GB');
      mock.verify();
    });
    it('should pass all parameters', function() {
      fetchMock
      .withArgs('https://baseurl/v1/onesearch?q=searchTerm&g=true&i=index&limit=3&offset=10&country=GB')
      .once();

      target.oneSearch('searchTerm', true, 'index', 3, 10, 'GB');
      mock.verify();
    });
  });
});
