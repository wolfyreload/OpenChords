pushd %~dp0
pandoc help.md -o help.html --css=HelpImages/help.css --number-section --toc
popd
