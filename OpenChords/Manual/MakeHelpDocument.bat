pushd %~dp0
..\packages\Pandoc.Windows.2.1.0\tools\Pandoc\pandoc.exe help.md -o help.html --css=media/github-pandoc.css --number-section --toc --self-contained
popd
