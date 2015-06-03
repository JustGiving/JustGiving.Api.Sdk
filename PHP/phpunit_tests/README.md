# Testing

this ports most of the JustGiving php api tests (excluding the some of the charity tests) to [php_unit](https://phpunit.de/).
At the time of writing , its 40 tests, 53 assertions that all pass.

## running these tests.
you'll need composer installed to fetch the dependencies, you can then run 


```
composer install
```

this gets you all the requirements. The phpunit executable will be in ./vendor/bin/phpunit.
A phpunit.xml file is supplied, so you can then do  


```
./vendor/bin/phpunit 
```

which runs the tests.




Author: Tim Marsh - tim@positivestudio.co.uk

Date: 3/June/2015
