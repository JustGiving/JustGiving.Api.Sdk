class Pagination {
  constructor(pageNum, pageSize) {
    this.pageSizeRestriction = pageSize?`pageSize=${pageSize}&`:'';
    this.pageNumRestriction = pageNum?`pageNum=${pageNum}&`:'';
    this.pageRestriction = pageNum?`page=${pageNum}&`:'';
    this.limitRestriction = pageSize?`limit=${pageSize}&`:'';
    this.offsetRestriction = pageNum?`offset=${pageNum}&`:'';
  }
}

export class ApiClient {
  constructor(url, appId, accessToken) {
    if (typeof url !== 'string') throw new Error('URL is required');
    if (url.indexOf('https') !== 0) throw new Error('Please use https only');
    this._url = url;
    this._appId = appId;
    this._accessToken = accessToken;
    this._version = 'v1';
  }

  _getOptions(payload, method) {
    const options = {method: method || 'GET', headers: {'x-app-id': this._appId, Accept: 'application/json'}};
    if (this._accessToken) {
      options.headers['Authorization'] = this._accessToken;
    }
    if (payload || method === 'POST') {
      options.method = method || 'POST';
      options.body = JSON.stringify(payload);
      options.headers['Content-Type'] = 'application/json';
    }
    return options;
  };

  _handleResponse(response) {
    if (response.status >= 400) {
      const contentType = response.headers.get('content-type');

      if(contentType && contentType.indexOf('application/json') === 0) {
        return response.json().then(json => {
          if (json[0]) {
            throw new Error(`${response.status} ${response.statusText}. ${json[0].id} : ${json[0].desc}`);
          }
        });
      }

      throw new Error(`${response.status} ${response.statusText}`);
    }

    return response.json();
  };

  _fetch(resource, payload, method) {
    return fetch(`${this._url}/${this._version}/${resource}`, this._getOptions(payload, method)).then(this._handleResponse);
  }

  // Account resource
  validateAccount(email, password) {
    return this._fetch('account/validate', {email: email, password: password});
  }

  getFundraisingPagesForUser(email, charityId) {
    const charityRestriction = (charityId?`?charityId=${charityId}`:'');
    return this._fetch(`account/${email}/pages${charityRestriction}`);
  }

  getDonationsForUser(pageSize, pageNum, charityId) {
    const charityRestriction = (charityId?`charityId=${charityId}&`:'');
    const pagination = new Pagination(pageNum, pageSize);
    return this._fetch(`account/donations?${pagination.pageSizeRestriction}${pagination.pageNumRestriction}${charityRestriction}`);
  }

  checkAccountAvailability(email) {
    return this._fetch(`account/${encodeURIComponent(email)}`);
  }

  getContentFeed() {
    return this._fetch('account/feed');
  }

  getAccountRating(pageSize, pageNum) {
    const pagination = new Pagination(pageNum, pageSize);
    return this._fetch(`account/rating?${pagination.pageSizeRestriction}${pagination.pageRestriction}`);
  }

  getAccount() {
    return this._fetch('account');
  };

  getInterests() {
    return this._fetch('account/interest');
  }

  addInterest(interest) {
    return this._fetch('account/interest', {interest: interest});
  }

  replaceInterests(...interests) {
    return this._fetch('account/interest', interests, 'PUT');
  }

  requestPasswordReminder(email) {
    return this._fetch(`account/${email}/requestpasswordreminder`);
  }

  changePassword(email, currentPassword, newPassword) {
    if (!email || !currentPassword || !newPassword) throw new Error('All parameters are required');
    return this._fetch(`account/changePassword?emailaddress=${email}&currentpassword=${encodeURIComponent(currentPassword)}&newpassword=${encodeURIComponent(newPassword)}`, undefined, 'POST');
  }

  // Countries resource
  getCountries() {
    return this._fetch('countries');
  };

  // Currency resource
  getCurrencies() {
    return this._fetch('fundraising/currencies');
  };

  // Charity resource
  getCharityCategories() {
    return this._fetch('charity/categories');
  };

  getCharity(charityId) {
    return this._fetch(`charity/${charityId}`);
  }

  getEventsByCharity(charityId, pageSize, pageNum) {
    const pagination = new Pagination(pageNum, pageSize);
    return this._fetch(`charity/${charityId}/events?${pagination.pageSizeRestriction}${pagination.pageNumRestriction}`);
  }

  // Donation resource
  getDonation(donationId) {
    return this._fetch(`donation/${donationId}`);
  }

  getDonationByReference(thirdPartyReference) {
    return this._fetch(`donation/ref/${encodeURIComponent(thirdPartyReference)}`);
  }

  getDonationStatus(donationId) {
    return this._fetch(`donation/${donationId}/status`);
  }

  // Event resource
  getEvent(eventId) {
    return this._fetch(`event/${eventId}`);
  }

  getEventPages(eventId, pageSize, pageNum) {
    const pagination = new Pagination(pageNum, pageSize);
    return this._fetch(`event/${eventId}/pages?${pagination.pageSizeRestriction}${pagination.pageRestriction}`);
  }

  registerEvent(eventDetails) {
    return this._fetch('event', eventDetails);
  }

  // Fundraising resource
  getFundraisingPages() {
    return this._fetch('fundraising/pages');
  }

  getFundraisingPage(pageShortName) {
    return this._fetch(`fundraising/pages/${encodeURIComponent(pageShortName)}`);
  }

  suggestPageShortName(preferredName) {
    return this._fetch(`fundraising/pages/suggest?preferredName=${encodeURIComponent(preferredName)}`);
  }

  checkPageExists(pageShortName) {
    return this._fetch(`fundraising/pages/${pageShortName}`, undefined, 'HEAD');
  }

  cancelFundraisingPage(pageShortName) {
    return this._fetch(`fundraising/pages/${pageShortName}`, undefined, 'DELETE');
  }

  addFundraisingPageImage(pageShortName, caption, url, isDefault) {
    return this._fetch(`fundraising/pages/${pageShortName}/images`, {caption: caption, isDefault: !!isDefault, url: url}, 'PUT');
  }

  // OneSearch resource
  onesearch(searchTerm, grouping, index, pageSize, pageNum, country) {
    return this._fetch(`onesearch?q=${encodeURIComponent(searchTerm)}&g=${encodeURIComponent(grouping)}&i=${encodeURIComponent(index)}&limit=${pageSize}&offset=${pageNum}&country=${country}`);
  }

  // Project resource
  getProject(projectId) {
    return this._fetch(`project/global/${projectId}`);
  }

  // Search resource
  searchCharities(searchTerm, charityId, categoryId, pageNum, pageSize) {
    const pagination = new Pagination(pageNum, pageSize);
    const charityIdRestriction = charityId.length?charityId.map(id => `charityId=${id}&`).join(''):`charityId=${charityId}&`;
    const categoryIdRestriction = categoryId.length?categoryId.map(id => `categoryId=${id}&`).join(''):`categoryId=${categoryId}&`;
    return this._fetch(`charity/search?q=${encodeURIComponent(searchTerm)}&${categoryIdRestriction}${charityIdRestriction}${pagination.pageSizeRestriction}${pagination.pageRestriction}`);
  }

  searchEvents(searchTerm, pageNum, pageSize) {
    const pagination = new Pagination(pageNum, pageSize);
    return this._fetch(`event/search?q=${encodeURIComponent(searchTerm)}&${pagination.pageSizeRestriction}${pagination.pageRestriction}`);
  }

  // OneSearch
  oneSearch(searchTerm, group, index, limit, offset, country) {
    const pagination = new Pagination(offset, limit);
    return this._fetch(`onesearch?q=${searchTerm}&g=${group}&i=${index}&${pagination.limitRestriction}${pagination.offsetRestriction}country=${country}`);
  }

  // Team resource
  getTeam(shortName) {
    return this._fetch(`team/${encodeURIComponent(shortName)}`);
  }

  checkTeamExists(shortName) {
    return this._fetch(`team/${encodeURIComponent(shortName)}`, undefined, 'HEAD');
  }

  createOrUpdateTeam(shortName, details) {
    return this._fetch(`team/${encodeURIComponent(shortName)}`, details, 'PUT');
  }

  joinTeam(teamShortName, pageShortName) {
    return this._fetch(`team/join/${encodeURIComponent(teamShortName)}`, {pageShortName: pageShortName}, 'PUT');
  }
}
