@echo off

echo Warning: all instances of Chrome, Edge, Firefox (browser and driver), and Selenium Manager are about to be termianted. Press any key to continue, or click close (X in upper right) to stop.
pause

REM Kill Chrome
taskkill /f /im chrome.exe
taskkill /f /im chromedriver.exe

REM Kill Edge
taskkill /f /im msedge.exe
taskkill /f /im msedgedriver.exe

REM Kill Firefox
taskkill /f /im firefox.exe
taskkill /f /im geckodriver.exe

echo All instances of Chrome, Edge and Firefox, along with their drivers, have been terminated.

REM Kill Selenium Manager
taskkill /f /im selenium-manager.exe

echo All instances of Selenium Manager have been terminated.

pause