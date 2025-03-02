# Descripción del juego

Este juego, creado en Unity, es un Maze-Runner con temática de Dungeons and Dragons multijugador, en el cual se generan laberintos con dimensiones nxn impares totalmente nuevos en cada partida. Para ganar en este juego, tienes que encontrar una llave esparcida en el laberinto y llegar al centro, donde te aguarda un gran tesoro. Pero por el camino, te encontrarás distintas trampas que te obstaculizarán en tu camino a la victoria y distintos beneficios que te ayudarán a superar los obstáculos.

# Lógica de la generación del mapa. 

Para la guía al abrir el juego, diríjase al botón "Guía" antes de empezar a jugar.La base de la generación de este mapa es el algoritmo de Prim, pero adaptado para la generación de laberintos en Unity. El laberinto comienza con una celda de camino inicial, donde a partir de ahí se va a empezar a generar el laberinto. Después se agregan a una lista las posibles paredes adyacentes a esta celda. Después, mediante un While, se selecciona una pared aleatoria de la lista y verifica si al otro lado de la pared no hay laberinto. Si no hay, convierte la posible pared que estaba en la lista y la celda del otro lado en camino. Como añadí otro camino, se agregan a la lista las posibles paredes adyacentes a los caminos, las cuales pueden ser tomadas en otra iteración. Repetimos este proceso hasta que no haya paredes en la lista. Una vez terminado, las celdas que no se hayan convertido en paredes se instancian como pared. Además, creo una matriz para guardar la instancia del laberinto.

# Instanciar jugadores

Para instanciar la cantidad de jugadores deseada, se obtiene a través de una variable que se guarda previamente cuando el jugador elige la cantidad, y después se recorre la matriz para instanciarlos en cada esquina del mapa. La instancia de los jugadores se la agrego a la matriz creada previamente.

# Instanciar trampas, buff, llaves y cofre

Para instanciar las trampas y buffs, se agregan a una lista los prefabs. Luego voy recorriendo la matriz y, en cada casilla "camino" que no haya jugador, ni llave, ni cofre, se va cogiendo un prefab aleatorio de la lista y se genera un número aleatorio. En dependencia de cuál sea, se instancia o no la trampa. Para instanciar las llaves, se generan dos números aleatorios dentro del rango del laberinto, lo cual te da una posición aleatoria del mapa que, mediante un While, se comprueba que no se instancie ni en paredes, ni en jugadores, ni en trampas, ni en el cofre. El cofre se instancia en la posición del centro.

# Controlador de turnos

Para controlar los turnos, agrego a una lista los jugadores y, mediante una variable, en cada iteración le sumo uno y le aplico el módulo en correspondencia con la cantidad de jugadores. Así voy recorriendo la lista y al jugador en el que esté la variable se le va modificando una variable booleana, la cual le permitirá moverse y activar habilidades.

# Movimiento del jugador

Para el movimiento del jugador, utilizo una función de Unity la cual mueve un objeto de su posición actual a la posición indicada a cierta velocidad. Se verifica que la dirección en la que se vaya a mover no sea una pared y solo se le permite moverse si es su turno, mediante la variable ya hablada en el controlador de turnos.

# Activación de trampas, llaves y cofre

Para la activación de la trampa, voy comprobando cada vez que se mueve el personaje en turno si la posición en la que se encuentra es la misma que la de una trampa. Si lo es, que llame a la función de activación que, en dependencia del nombre de la trampa, mediante un switch se elige lo que tiene que hacer la trampa. Para la llave, compruebo igual de manera que la trampa, pero en vez de activar una función, lo que hago es volver una variable booleana true, que es la que me permite después abrir el cofre para finalizar el juego.

# Activación de habilidades

Para activar habilidades, le puse un input que al tocarlo, en dependencia de la habilidad que el personaje tenga, mediante un switch se le aplican los efectos que tiene que tener la habilidad.

# Lógica de la cámara

Para la cámara, compruebo mediante el índice hablado anteriormente en el controlador de turno que el jugador en el que está el índice actual, la cámara le haga seguimiento con una función llamada Vector3.Lerp.
