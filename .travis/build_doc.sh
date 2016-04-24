#!/bin/sh

src_dir="doc/"
trg_dir="doc/pdf"
now="$(TZ='Europe/Berlin' date +'%d.%m.%Y')"

for f in $(find $src_dir -type f -name "*.adoc" ! -name "_*.adoc"); do
	tmp="${f##*/}"
	filename=${tmp%.*}
	asciidoctor-pdf -a pdf-stylesdir=doc/themes -a pdf-style=blue -a pdf-fontsdir=doc/fonts -a localdate=$now $f -o "$trg_dir/$filename.pdf";
done

cd $trg_dir
zip Dokumentation.zip *.pdf
