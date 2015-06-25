[![Build Status](https://travis-ci.org/mathi123/dotnet.svg?branch=test)](https://travis-ci.org/mathi123/dotnet)

# Odoo C# projects

## These libraries allow you to:

* connect to Odoo over XMLRpc.
* annotate your C# classes with **OdooPropery**, **OdooMany2one** and **OdooOne2many** attributes.
* search/read/write/update typed classes automatically to Odoo server using Repository pattern 

## Guidelines

* This repository is a visual studio project
* OdooRpcWrapper is the core library who transforms the attribute annotations in XMLRpc structs
* OdooTypedClasses is a class library containing some example classes that you can use or inherit from, you can configure your own classes from scratch.

## Issues

Please report issues on [github](https://github.com/OCA/dotnet/issues)

## Screenshots

![User](/doc/img/Screenshot-User-Class.JPG)

![Partner](/doc/img/Screenshot-Partner-Class.JPG)
