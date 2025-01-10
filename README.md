# Cat3_API

Este proyecto es un backend desarrollado en .NET 8.0, diseñado para manejar la autenticación de usuarios y la gestión de posts con integración a Cloudinary.

## **Requisitos previos**

1. **SDK de .NET 8.0**:
   - Descargar e instalar desde [dotnet.microsoft.com](https://dotnet.microsoft.com/).

2. **Base de datos SQLite**:
   - Preinstalado con el SDK de .NET.

3. **Configuración de Cloudinary**:
   - Crear una cuenta en [Cloudinary](https://cloudinary.com/).
   - Obtener los valores de `CloudName`, `ApiKey` y `ApiSecret`.

4. **Postman**:
   - Opcional para probar los endpoints.

---

## **Instalación**

### **Clonar el repositorio**
```
git clone https://github.com/DavidZeballos/IDWM_Catedra3Api
cd Cat3_API
```

### **Instalar dependencias**
```
dotnet restore
```

---

## **Configuración**

### **Variables de entorno**
Crea un archivo `.env` en la raíz del proyecto con el siguiente contenido:

```env
DB_CONNECTION_STRING=Data Source=posts.db
```

### **Archivo `appsettings.json`**
Asegúrate de que `appsettings.json` contenga la configuración correcta para JWT y Cloudinary:

```json
{
  "JWT": {
    "Issuer": "http://localhost:5012",
    "Audience": "http://localhost:5012",
    "SigningKey": "EstaEsUnaClaveDeFirmaSuficientementeLargaQueSuperaLos64Caracteres1234567890"
  },
  "CloudinarySettings": {
    "CloudName": "ReemplazaConTuCloudName",
    "ApiKey": "ReemplazaConTuApiKey",
    "ApiSecret": "ReemplazaConTuApiSecret"
  },
  "AllowedHosts": "*"
}
```

---

## **Comandos para ejecutar**

1. **Ejecutar el proyecto**:
   ```
   dotnet run
   ```

2. **Limpieza y reconstrucción**:
   ```
   dotnet clean
   dotnet build
   ```

---

## **Pruebas**

### **Endpoints disponibles**
1. **Registro de usuario**:
   - **POST**: `/api/auth/register`
   - Body (JSON):
     ```json
     {
       "email": "usuario@example.com",
       "password": "Contraseña123"
     }
     ```

2. **Inicio de sesión**:
   - **POST**: `/api/auth/login`
   - Body (JSON):
     ```json
     {
       "email": "usuario@example.com",
       "password": "Contraseña123"
     }
     ```

3. **Crear un post**:
   - **POST**: `/api/posts`
   - Headers: `Authorization: Bearer <token>`
   - Body (form-data):
     - `Title`: "Mi primer post"
     - `Image`: [Seleccionar archivo JPG/PNG]

4. **Obtener posts**:
   - **GET**: `/api/posts`
   - Headers: `Authorization: Bearer <token>`
