#!/bin/sh

src_dir="doc/src"
trg_dir="doc/build"

for f in $(find $src_dir -type f -name "*.adoc"); do
	tmp="${f##*/}"
	filename=${tmp%.*}
	asciidoctor-pdf $f -o "$trg_dir/$filename.pdf";
done
