# PROGRAMACION3UMG
===============================
README - FUNCIONAMIENTO DEL PROGRAMA
===============================

Este programa permite gestionar clientes y tarjetas de crédito, realizar transacciones, renovar tarjetas, cambiar el PIN y más. A continuación, se detalla paso a paso cómo funciona el programa.

-------------------------------
1. INICIO DEL PROGRAMA
-------------------------------
- Se preparan tres listas principales:
  - Lista de clientes.
  - Lista de transacciones pendientes.
  - Lista del historial de transacciones.

- Se intenta cargar la información de los clientes desde un archivo.
- Si hay un error (por ejemplo, el archivo no existe), se detiene el programa.

-------------------------------
2. MENÚ PRINCIPAL
-------------------------------
Se muestra un menú con las siguientes opciones:

[1] Buscar cliente
[2] Ver tarjetas de cliente
[3] Agregar transacción a tarjeta
[4] Ver historial de transacciones
[5] Procesar transacciones pendientes
[6] Mostrar todos los clientes y sus tarjetas
[7] Renovar tarjeta
[8] Cambiar PIN de tarjeta
[9] Bloquear o desbloquear tarjeta
[0] Salir

Cada opción realiza una acción distinta:

-------------------------------
[1] BUSCAR CLIENTE
-------------------------------
- Se pide el ID del cliente.
- Si existe, se muestra el nombre del cliente.
- Si no, se indica que no fue encontrado.

-------------------------------
[2] VER TARJETAS DE CLIENTE
-------------------------------
- Se pide el ID del cliente.
- Se muestran sus tarjetas con:
  - Número de tarjeta
  - Límite de crédito
  - Saldo disponible
  - Fecha de vencimiento
  - Estado (bloqueada o activa)
  - Código PIN

-------------------------------
[3] AGREGAR TRANSACCIÓN A TARJETA
-------------------------------
- Se busca el cliente por su ID.
- Luego se pide el número de tarjeta.
- Se pregunta:
  - Tipo de transacción (compra o pago)
  - Monto
  - Fecha
- Se crea la transacción y se agrega al historial y a la lista de pendientes.

-------------------------------
[4] VER HISTORIAL DE TRANSACCIONES
-------------------------------
- Se muestran todas las transacciones de todas las tarjetas del cliente.
- Se incluyen detalles como tipo, fecha y monto.

-------------------------------
[5] PROCESAR TRANSACCIONES PENDIENTES
-------------------------------
- Se ejecutan todas las transacciones pendientes.
- Se muestra un mensaje por cada una procesada.

-------------------------------
[6] MOSTRAR TODOS LOS CLIENTES Y SUS TARJETAS
-------------------------------
- Se muestran todos los clientes registrados.
- Por cada cliente se listan sus tarjetas y sus transacciones.

-------------------------------
[7] RENOVAR TARJETA
-------------------------------
- Se busca el cliente y la tarjeta.
- Se actualiza la fecha de vencimiento de la tarjeta.

-------------------------------
[8] CAMBIAR PIN DE TARJETA
-------------------------------
- Se busca el cliente y la tarjeta.
- Se solicita un nuevo PIN y se actualiza.

-------------------------------
[9] BLOQUEAR O DESBLOQUEAR TARJETA
-------------------------------
- Si la tarjeta está activa, se bloquea.
- Si está bloqueada, se desbloquea.

-------------------------------
[0] SALIR
-------------------------------
- Finaliza el programa.

-------------------------------
FIN DEL PROGRAMA
-------------------------------
- Después de cada opción, se pausa el sistema para que el usuario pueda leer los resultados antes de continuar.

