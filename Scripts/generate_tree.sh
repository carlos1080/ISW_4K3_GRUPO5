#!/bin/bash

# Genera la estructura del directorio y la guarda en un archivo temporal
tree -L 2 --noreport > directory_structure.txt

# Inserta la estructura del directorio en el README.md
awk '/<!-- START DIRECTORY TREE -->/{print;system("cat directory_structure.txt");next}1' README.md > README_tmp.md
mv README_tmp.md README.md

# Limpia el archivo temporal
rm directory_structure.txt
