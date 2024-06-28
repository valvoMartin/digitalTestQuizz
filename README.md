# Roles de Usuarios
  - Admin        => Le permito ver todo
  - User         => Tiene acceso a la pagina de Sistema
  - Anonimous    => Un usuario que todavia no se ha registrado 

# PAGINA PRINCIPAL
  - La pagina de bienvenida muestra la pagina principal con un header arriba, con logo de la empresa de un lado y Singing y Login a la derecha
  - banner de la empresa debajo
  - Info introductoria para hacer el test y los pros

# PAGINA SISTEMA
  - Todas las paginas van a tener un Header con Login y Register y SobreNosotros(Te redirige hacia la PaginaPrincipal)
  - En caso de que el usuario ya haya terminado el test se le habilitara la seccion de ver resultados/estadisticas

# PAGINA DE VER-RESULTADOS
  - Para esta pagina visualizar los graficos indicativos de tu empresa, comparacion entre distintas areas, comparacion entre distintas TipoPreguntas
  - Ver Fortalezas y Debilidades de tu empresa con el grafico.
  - Permitir filtrar, ver Comparacion entre las TipoPregunta de entre distintas Areas 

## USUARIOS

### Altas
  - Cuando se da de alta un usuario, crea su empresa
  - Un usuario solo puede crear una empresa
  - No solo pone sus datos de Usuario, tambien pone los de su empresa
  - Cuando un nuevo usuario quiere crear una empresa, ingresa el CUIT, esa es la PK, si  ya existe significa q otro compañero de el ya la registro. en este caso solo selecciona el cuit ese y me precarga los datos sin poder editar los campos. Solo le permito cambiar los campos de su empresa una vez logeado el usuario.
  - Campo SECTOR debe ser tipiado por el usuario ej director, jefe, etc

### Modificacion
  - Puede modificar los datos de su usuario, y de la empresa que creo

### VISTAS
  - La pagina del Login ingresa el mail y contraseña, Debajo opciones para ir a registrarse o reestablecer contraseña. Esta vista podria ser una vista mmodal
  - La pagina de Register, vos sas de alta el usuario y tambien tu empresa. Esta vista podria ser una vista mmodal

## EMPRESAS
- Una misma empresa puede estar dos vecen en la tabla empresa, son de distintos usuarios dentro del sistema (Que hago con la PK del CUIT en este caso??)
- Podriamos hacer una comparacion entre usuarios de misma empresa, si es que los hay
- Un usuario con su empresa cuando termina de contestar tiene un nivel asignado en base al puntaje que obtuvo. Nivel relacionado a cada usuario
  - 0 = Inicial
  - 1 = Novato
  - 2 = Competente
  - 3 = Avanzado
  - 4 = Experto
  # CAMPOS de Company
  --

## AREAS
- Cada area es unica y no es repetible
- Ej.
  - InfoGral
  - DireccionYGestionEstrategica
  - Administracion
  - Finanzas
  - CapHumano
  - Operaciones
  - I+D
  - TransfDigital

## SUB-AREAS
- Cada sub-area tiene secciones que se van a repetir o no dentro de otras sub-areas
  Ej.
    - Operaciones
      - Planificacion
      - Produccion Gestion
      - Calidad
    - TransfDigital
      - Adminstracion
      - Finanzas

## PREGUNTAS
- Todas las preguntas son MChoise.

### VISTAS
  - Para ver las preguntas en pantalla, tener una vista que solo veas la pregunta que estas haciendo con las opciones de respuesta, arriba mostrar area y SubArea donde estas ubicado y el Procentaje que venis completando para orientarte.
  - Tener un boton de siguientePregunta y anteriorPregunta debajo. Cuando le das siguiente la direccion de la pagina no cambia, simplemente carga nuevamente con la nueva pregunta, o carga nuevamente ese componente.
    
## RESPUESTAS
- Las respuestas a cada pregunta va a estar vinculada con el usuario, no a cada empresa. Para permitir guardar distintas respuestas de usuarios de misma empresa
- Algunas Respuestas son a nivel de indicar que tanto te sentis identificado(VER como hacerlo de forma agradable)

### VISTAS
  - Para el componente de preguntas tener en cuenta que hay algunas que tenes q indicar un nivel o una cantidad de que tanto te sentis identificado(Ver comom implementar esto de forma agradable a la vista)  

## FILTROS


# DUDAS
  - Como es la mejor manera de hacer una carga de datos que conlleva, muchos datos a ingresar. Donde esta carga se deberia hacer por pasos, en la misma ruta.
  - 
