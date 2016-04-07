#!/bin/sh

setup_git() {
  git config --global user.email "fried_ice@travis-ci.org"
  git config --global user.name "Travis CI"
  git remote add upstream https://${GH_TOKEN}@github.com/fried-ice/Medida_2016_11.git > /dev/null 2>&1
}

commit_website_files() {
  git fetch --all
  git stash
  git checkout doc-travis
  git stash apply && git stash drop
  cd doc/pdf
  git add . *.pdf
  git commit --message "Travis asciidoctor build: $TRAVIS_BUILD_NUMBER"
}

upload_files() {
  
  git push --quiet upstream doc-travis
}

setup_git
commit_website_files
upload_files
