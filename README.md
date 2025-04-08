# ProductApi

Este proyecto es una API REST en .NET que gestiona productos.  
**Objetivo de la prueba técnica:**  
El entrevistado deberá identificar y corregir el siguiente bug en la lógica de negocio:

- **Regla de negocio:** Si la cantidad de un producto es **mayor a 10**, se debe aplicar un descuento del 10% sobre el total.

## Cómo Ejecutar la API

1. Clonar el repositorio.
2. Abrir el proyecto en Visual Studio o Visual Studio Code.
3. Restaurar los paquetes NuGet. dotnet restore
4. Ejecutar la aplicación.   dotnet run
   La API se iniciará y estará disponible en `https://localhost:{puerto}`.
   OpenApi: https://localhost:{puerto}/openapi/v1.json

## Endpoints Disponibles

- **GET** `/api/products`  
  Obtiene la lista de productos.

- **GET** `/api/products/{id}`  
  Obtiene un producto por ID.

- **POST** `/api/products`  
  Crea un nuevo producto.  
  Ejemplo de JSON:
  ```json
  {
    "name": "Producto A",
    "price": 100.0,
    "quantity": 5
  }

## Endpoints Disponibles

GET /api/products : Obtiene la lista de productos.

GET /api/products/{id} : Obtiene un producto por ID.

POST /api/products : Crea un nuevo producto.

PUT /api/products/{id} : Actualiza un producto existente.

DELETE /api/products/{id} : Elimina un producto.

GET /api/products/total/{id} : Calcula el total del producto aplicando la regla de negocio.

## Prueba 2: Implementación de Validaciones con FluentValidation
**Objetivo:** Evaluar la capacidad del entrevistado para implementar validaciones robustas en los modelos de datos utilizando la biblioteca FluentValidation.​

**Descripción del Problema:**

Actualmente, la API permite la creación y actualización de productos sin restricciones estrictas en los datos ingresados, lo que podría llevar a inconsistencias en la base de datos. Se requiere implementar las siguientes validaciones en el modelo Product utilizando FluentValidation:​

    **Nombre del Producto (Name):**

        No debe estar vacío ni ser nulo.​

        Debe tener una longitud máxima de 100 caracteres.​

    **Precio (Price):**

        Debe ser un valor positivo mayor que cero.​

    **Cantidad (Quantity):**

        Debe ser un número entero positivo o cero.

## Prueba 3: Optimización del Rendimiento en la Recuperación de Datos

**Objetivo:** Evaluar la habilidad del entrevistado para identificar y corregir problemas de rendimiento relacionados con la recuperación de datos en la API.​

**Descripción del Problema:**

Se ha identificado que el método GetAll en el servicio ProductService presenta problemas de rendimiento cuando la cantidad de productos en la base de datos es considerable. Actualmente, este método recupera todos los productos sin paginación, lo que puede generar una carga innecesaria en el servidor y tiempos de respuesta elevados.        




## Cambios Realizados Durante la Prueba

### ✅ Prueba 1: Corrección de Lógica de Descuento

- **Archivo Modificado:** `Services/ProductService.cs` → Método `CalculateTotal`  
- **Cambio:** Se eliminó el operador `>=` para que el descuento del 10% se aplique **solo cuando la cantidad sea estrictamente mayor a 10**, cumpliendo con la regla de negocio.

---

### ✅ Prueba 2: Implementación de Validaciones con FluentValidation

- **Archivo Nuevo:** `Validator/ProductValidator.cs`  
- **Archivo Modificado:** `Program.cs`  
- **Cambios:**
  - Se integró FluentValidation para validar los campos del modelo `Product`.
  - Se agregaron las siguientes reglas:
    - `Name`: requerido, máximo 100 caracteres.
    - `Price`: mayor que 0.
    - `Quantity`: entero positivo o cero.
  - En `Program.cs` se añadieron las líneas:
    ```csharp
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
    ```

---

### ✅ Prueba 3: Optimización de Datos con Paginación

- **Archivo Nuevo:** `Models/Common/PagedResult.cs`  
- **Archivo Modificado:** `Controllers/ProductsController.cs`  
- **Cambio:** El método `GetAll()` ahora acepta parámetros opcionales `page` y `pageSize`, devolviendo datos paginados.  
  - Si no se envían parámetros, devuelve la **primera página por defecto** con 10 productos.
  - La respuesta incluye información sobre la página actual, total de productos y total de páginas.
