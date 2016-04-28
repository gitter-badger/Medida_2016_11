@ ECHO OFF

echo Rendering PDFs...
for %%a in ("..\doc\*.adoc") do asciidoctor-pdf -a pdf-stylesdir=../doc/themes -a pdf-style=blue -a pdf-fontsdir=../doc/fonts "%%a" -o  "..\doc\pdf\%%~na.pdf"
echo Done.
