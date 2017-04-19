define([
  '../person'
], function (person) {
  'use strict';
  
  var personAge = function (id, firstName, lastName, age, group) {
    person.call(this, id, firstName, lastName, age);
    this.group = group;
  }

  personAge.prototype = new person();

  return personAge;
});
