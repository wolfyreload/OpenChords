pushd %~dp0
pandoc help.md -o help.html --css=media/github-pandoc.css --number-section --toc --self-contained
popd
