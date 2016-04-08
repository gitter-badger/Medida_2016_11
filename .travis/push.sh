#!/bin/sh

setup_git() {
  git config --global user.email "fried_ice@travis-ci.org"
  git config --global user.name "Travis CI"
  git remote add upstream https://${GH_TOKEN}@github.com/fried-ice/Medida_2016_11.git > /dev/null 2>&1
  git fetch --all
}

commit_website_files() {
  git checkout doc
  cd doc/pdf
  git add . *.pdf
  git stash
  git checkout doc-travis
  git checkout stash -- .
  git stash drop
  git commit --message "Travis asciidoctor build: $TRAVIS_BUILD_NUMBER"
}

upload_files() {

  git push --quiet upstream doc-travis
}

setup_git
commit_website_files
upload_files
