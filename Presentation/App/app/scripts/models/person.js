define([
  '../models/model'
], function (model) {
  'use strict';
  var person = function (id, firstName, lastName, age) {
    model.call(this);
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.age = age;
    this.validateResult = [];
  }

  //inherit stuffs from model
  person.prototype = new model();

  person.prototype.setVersion = function (version) {
    this._rowVersion = version;
  };
  person.prototype.validate = function () {
    if (this.firstName === null)
      this.validateResult.push({
        prop: "firstName",
        msg: "is null"
      });
    if (this.lastName === null)
      this.validateResult.push({
        prop: "lastName",
        msg: "is null"
      });
    if (this.age === null)
      this.validateResult.push({
        prop: "age",
        msg: "is null"
      });
    else if (!Number.isInteger(this.age))
      this.validateResult.push({
        prop: "age",
        msg: "is not integer"
      });
    else if (this.age < 0 || this.age > 2147483647)
      this.validateResult.push({
        prop: "age",
        msg: "is not in [0, 2147483647]"
      });
  };

  return person;
});
