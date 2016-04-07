#!/bin/sh

src_dir="doc/"
trg_dir="doc/pdf"

for f in $(find $src_dir -type f -name "*.adoc"); do
	tmp="${f##*/}"
	filename=${tmp%.*}
	asciidoctor-pdf $f -o "$trg_dir/$filename.pdf";
done
