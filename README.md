<div>
  <h1 align="center">Universidad Tecnologica Nacional</h1>
  <h2 align="center">Ingeniería y Calidad de Software</h2>
  <p align="center">
  <img src="./Imagenes/IMG_UTN_logo.jpg" alt="Logo del Proyecto" width="200">
  </p>

</div>

## Año: 2024
## Curso: 4k3
## Docentes: 

- Laura Ines Covaro
- Maria Cecilia Massano
- Georgina Gisela Gonzalez

## Integrantes del grupo: 

- Carlos Nayi - 90406
- Genaro Bergesio - 83464
- Marcelo Cuello - 83296
- Agustin Liendo - 90080
- Tomas Galiano - 85824

## Tree

```
.
├── Bibliografia
│   └── test.txt
├── Filminas
│   ├── filminas_clase_1_SCM.pdf
│   └── filminas_clase_2_UserStories.pdf
├── Imagenes
│   ├── ISW_encabezado.png
│   ├── UTN_encabezado.png
│   └── UTN_logo.jpg
├── NotasDeClase
│   ├── 0823-V-1.jpeg
│   ├── 0823-V-2.jpeg
│   ├── 0828-M-1.jpeg
│   ├── 0828-M-2.jpeg
│   └── 0828-M-3.jpeg
└── README.md

```

## Introducción:

Adentrándanos en la última etapa de la carrera, nos topamos con un nuevo desafío, que es esta materia. Sin embargo, como grupo tenemos bien en claro que debemos aprovecharla al máximo, porque realmente creemos que nos va a aportar conocimientos útilies para nuestra vida laboral.

Lo que queremos lograr con este trabajo práctico es aprender a gestionar la configuracion de software mediante el uso de algún repositorio como lo es Github o Gitlab.

Recordamos que SCM es una actividad paraguas, transversal a todo el proyecto, relevante para el producto a lo largo de su ciclo de vida. Su **propósito** es establecer y mantener la integridad de los productos de software a lo largo de su ciclo de vida.

Es importante aclarar que como grupo consideramos que la riqueza de este trabajo práctico es mantener en uso y en constante actualizacion el repositorio, ya que no tendría sentido realizar un commit cada uno y dar por finalizado la entrega.


## Reglas de Nombrado

| Item de configuración | Regla de nombrado | Ejemplo | Ubicación Física |
|-----------|-----------|-----------|-----------|
| Bibliografía | Biblio_\<N>_\<NombreBiblio>.pdf | \Biblio_U1_Cynefin Mini book.pdf | /Bibliografia/U<N> |
| Filminas | PPT_\<N>_\<Tema>.pdf | filminas_clase_1_SCM.pdf | /Filminas |
| Imagenes | IMG_\<Categoria>_\<NombreImagen>.\<extImg> | ISW_encabezado.png | /Imagenes |
| NotasDeClase | NC_\<Fecha>-\<DiaCursado>-\<N>.\<extImg> | NC_0823-V-1.jpeg | /NotasDeClase|
| Guias | Guía_\<Descripción>.pdf | 0823-V-1.jpeg | /NotasDeClase|

## Glosario

| Sigla | Significado |
|--|--|
| \<N> | Número de item (1,2,3…) |
| \<Tema> | Tema de clase o trabajo práctico dado, como aparece en la modalidad académica | 
| \<NombreBiblio>  | Título de la bibliografía |
| \<Fecha> | Fecha en el formato mm-dd en el que fue tomado el elemento|
| \<DiaCursado> | Día en el formato (M/V) en el que fue tomado el elemento, donde M y V referencian los días de cursado Miércoles y Viernes |
| \<extImg> | Etensiones de archivos de imágenes, tipicamente como jpg, png, jpeg... |

## Baseline:
- Se utilizan etiquetas para marcar las baselines

- No debemos confundir con la versión del producto

- Entonces una línea base es una configuración que ha sido revisada formalmente y sobre la que se ha llegado un acuerdo

- Sirve como base para desarrollos posteriores y puede cambiarse sólo a través  de un procedimiento formal de control de cambios

- Permiten ir atrás en el tiempo y reproducir el entorno de desarrollo en un momento dado del proyecto

- Las líneas base puede ser:

  - De especificación DE (Requerimientos, Diseño)
  - De productos que han pasado por un control de calidad definido previamente

**¿En qué nos basamos como grupo para establecer nuestra baseline?**

Antes de cada entrega formal de un repositorio, en esta caso una entrega de trabajo práctico, en la que debemos dejar  marcado de alguna manera nuestro prograso actual, para luego en otra entrega de otro trabajo practico generar otra baseline y así poder comparar el proyecto en dos instancias de tiempo distintas.


## Github Actions:

### ¿Qué es una Github Action?

Una GitHub Action es una herramienta de automatización que permite integrar flujos de trabajo personalizados directamente en los repositorios de GitHub. Estas acciones pueden ser utilizadas para automatizar una variedad de tareas, como ejecutar pruebas, desplegar aplicaciones, verificar el código o actualizar la documentación.

GitHub Actions es extremadamente flexible y se basa en un sistema de eventos, donde las acciones se desencadenan automáticamente por eventos específicos, como un push a la rama principal o la creación de un pull request. Esto permite a los equipos de desarrollo optimizar sus procesos y mantener una alta eficiencia en la gestión del código

### ¿En qué consiste la GitHub Action readme-tree-writer?

La GitHub Action readme-tree-writer es una acción que automatiza la generación de la estructura de directorios de un proyecto en el archivo README.md de tu repositorio. Utiliza el comando tree para crear una representación visual de la jerarquía de carpetas y archivos, y luego inserta o actualiza esta estructura directamente en el README.md cada vez que se realiza un push en el repositorio. Esto es particularmente útil para mantener una documentación actualizada y coherente, reflejando siempre la estructura real del proyecto.

### Importancia de las GitHub Actions en el contexto del Software Configuration Management

En el contexto de la gestión de la configuración del software (Software Configuration Management o SCM), las GitHub Actions juegan un papel crucial al proporcionar automatización y consistencia en los procesos de desarrollo. La capacidad de automatizar tareas repetitivas, como pruebas, integraciones continuas, despliegues y actualizaciones de documentación, asegura que el proceso de desarrollo sea más eficiente y menos propenso a errores humanos.

Además, las GitHub Actions permiten mantener una trazabilidad clara de los cambios, garantizando que cada modificación en el código se maneje de manera coherente y controlada. Esto es esencial para mantener la integridad del código a lo largo del ciclo de vida del desarrollo y para garantizar que todas las partes del software se gestionen adecuadamente, reduciendo riesgos y mejorando la calidad del producto final.



