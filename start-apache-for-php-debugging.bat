@echo off
setlocal
set PATH=SET PATH=%PATH%;%cd%\PHP_Server\php-5.4.0-Win32-VC9-x86
@echo on
@echo Starting apache on http://localhost:678

PHP_Server\Apache22\bin\httpd.exe