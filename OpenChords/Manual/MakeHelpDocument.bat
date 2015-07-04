pushd %~dp0
pandoc help.md -o help.html --css=helpimages/help.css --toc
popd
