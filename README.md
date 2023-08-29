# RestaurantApp

 
 
* [Introduction](#Introduction)
* [Technologies](#Technologies)
* [Database](#Database)
* [Controllers](#controllers)




--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 
 # [Controllers](#controllers)
 
| Controller | Description |
|---|---|
| [GetAllCategories](#GetAllCategories)| Get all categories from the database. |
| `GetById` | Get a category by its ID from the database. |
| `AddCategory` | Add a new category to the database. |
| `UpdateCategory` | Update an existing category in the database. |
| `DeleteCategory` | Delete a category from the database. |


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


 # [CategoryController](#CategoryController)

## GetAllCategories Endpoint and Methods
The GetAllCategories endpoint is an HTTP GET method that retrieves all categories from the database using `GetAll` method . This endpoint is accessed by sending a GET request to the /Get All Categories route.

## GetAllCategories Endpoint

* Method: GET
* URL: `/GetAllCategories`
* Response:
    * A JSON array of categories, each of which is represented as a JSON object with the following properties:
        * `id`: The ID of the category.
        * `name`: The name of the category.
        * `dishes`: An array of dishes in the category, each of which is represented as a JSON object with the following properties:
            * `id`: The ID of the dish.
            * `name`: The name of the dish.
            * `description`: The description of the dish.
            * `price`: The price of the dish.
            * `available`: Whether the dish is available.
        * Example:

```
[
    {
        "id": 1,
        "name": "Appetizers",
        "dishes": [
            {
                "id": 1,
                "name": "French Fries",
                "description": "Golden brown, crispy fries served with ketchup.",
                "price": 3.99,
                "available": true
            },
            {
                "id": 2,
                "name": "Chicken Wings",
                "description": "Juicy, grilled chicken wings tossed in your choice of sauce.",
                "price": 10.99,
                "available": true
            }
        ]
    },
    {
        "id": 2,
        "name": "Main Courses",
        "dishes": [
            {
                "id": 3,
                "name": "Steak",
                "description": "A juicy, grilled steak served with your choice of sides.",
                "price": 24.99,
                "available": true
            },
            {
                "id": 4,
                "name": "Pizza",
                "description": "A large, hand-tossed pizza with your choice of toppings.",
                "price": 19.99,
                "available": true
            }
        ]
    }
]
```
### GetAll Method
The GetAll method is an asynchronous method that returns a List<Category>. This method uses the _context. Categories property to access the Categories DbSet and retrieve all categories from the database. The Include method is used to include related data, in this case, the Dishes navigation property. The AsNoTracking method is used to improve performance by disabling change tracking. The ToListAsync method is used to asynchronously execute the query and retrieve the results.

If an exception occurs while executing this method, it is logged using the _logger.LogError method and rethrown. The error message includes the text “An error occurred while using (GetAll) method CategoryServices”.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetCategoryById Endpoint and Methods
The GetCategoryById endpoint is an HTTP GET method that retrieves a category by its ID from the database using method `GetCategoryById`. This endpoint is accessed by sending a GET request to the /{id}/GetCategory route, where {id} is the ID of the category to retrieve.


## GetCategoryById Endpoint 

* Method: GET
* URL: `/GetCategoryById/{id}`
* Params:
    * `id`: The ID of the category.
* Response:
    * A JSON object representing the category, with the following properties:
        * `id`: The ID of the category.
        * `name`: The name of the category.
        * `dishes`: An array of dishes in the category, each of which is represented as a JSON object with the following properties:
            * `id`: The ID of the dish.
            * `name`: The name of the dish.
            * `description`: The description of the dish.
            * `price`: The price of the dish.
            * `available`: Whether the dish is available.
        * Example:

```
{
    "id": 1,
    "name": "Appetizers",
    "dishes": [
        {
            "id": 1,
            "name": "French Fries",
            "description": "Golden brown, crispy fries served with ketchup.",
            "price": 3.99,
            "available": true
        },
        {
            "id": 2,
            "name": "Chicken Wings",
            "description": "Juicy, grilled chicken wings tossed in your choice of sauce.",
            "price": 10.99,
            "available": true
        }
    ]
}
```
* Code: `404 Not Found`: The category with the specified ID was not found.
* Code: `200 OK`


### GetCategoryById Method
The GetCategoryById method is an asynchronous method that returns a Category object. This method takes an id parameter that specifies the ID of the category to retrieve. The method uses the _context.Categories property to access the Categories DbSet and retrieve the category with the specified ID from the database. The Include method includes related data, in this case, the Dishes navigation property. The FirstOrDefaultAsync method is used to asynchronously execute the query and retrieve the first result or a default value if no results are found.

If no category is found, the method throws an exception with a message indicating that no category was found. If an exception occurs while executing this method, it is logged using the _logger.LogError method and rethrown. The error message includes the text “An error occurred while using (GetById) method CategoryServices”.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## AddCategory Endpoint and Methods
The `AddCategory` endpoint is an HTTP POST method that adds a new category to the database using the `AddCategory` method. This endpoint is accessed by sending a POST request to the `/AddCategory` route with a JSON payload representing the category to add.

### AddCategory Endpoint
* **Method**: POST
* **URL**: `/AddCategory`
* **Body**:
    * A JSON object representing the category to add, with the following properties:
        * `Name`: The name of the category.
    * **Example**:

```json
{
    "Name": "Appetizers"
}
```
* **Response**:
    * A JSON object representing the added category, with the following properties:
        * `id`: The ID of the category.
        * `name`: The name of the category.
    * **Example**:

```json
{
    "id": 1,
    "name": "Appetizers"
}
```
* **Code**: `400 Bad Request`: The category already exists.
* **Code**: `200 OK`

### AddCategory Method
The `AddCategory` method is an asynchronous method that adds a new Category object to the database. This method takes a `model` parameter that specifies the Category object to add. The method uses the `_categoryServices.CategoryIsExist` method to check if a category with the same name already exists in the database. If a category with the same name already exists, the method returns a `BadRequest` result with a message indicating that the category already exists.

If no category with the same name exists, the method uses the `_categoryServices.AddCategory` method to add the new Category object to the database. The method then returns an `Ok` result with the added Category object.

### CategoryIsExist Method
The `CategoryIsExist` method is an asynchronous method that checks if a category with a specified name already exists in the database. This method takes a `categoryName` parameter that specifies the name of the category to check. The method uses the `_context.Categories.AnyAsync` method to check if any categories exist in the database with the specified name.

If an exception occurs while executing this method, it is logged using the `_logger.LogError` method and rethrown. The error message includes the text “An error occurred while using (CategoryIsExist) method CategoryServices”.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## UpdateCategory Endpoint and Method
The `UpdateCategory` endpoint is an HTTP PUT method that updates an existing category in the database using the methods  `UpdateCategory` and `CategoryIsExist` . This endpoint is accessed by sending a PUT request to the `/UpdateCategory` route with query parameters representing the ID of the category to update and a JSON payload representing the updated category.

### UpdateCategory Endpoint
* **Method**: PUT
* **URL**: `/UpdateCategory`
* **Query Params**:
    * `id`: The ID of the category to update.
* **Body**:
    * A JSON object representing the updated category, with the following properties:
        * `Name`: The updated name of the category.
    * **Example**:

```json
{
    "Name": "Appetizers"
}
```
* **Response**:
    * A JSON object representing the updated category, with the following properties:
        * `id`: The ID of the category.
        * `name`: The updated name of the category.
    * **Example**:

```json
{
    "id": 1,
    "name": "Appetizers"
}
```
* **Code**: `400 Bad Request`: The category already exists.
* **Code**: `200 OK`

### UpdateCategory Method
The `UpdateCategory` method is an asynchronous method that updates an existing Category object in the database. This method takes an `id` parameter that specifies the ID of the category to update and a `model` parameter that specifies the updated Category object. The method uses the `_categoryServices.CategoryIsExist` method to check if a category with the same name already exists in the database. If a category with the same name already exists, the method returns a `BadRequest` result with a message indicating that the category already exists.

If no category with the same name exists, the method uses the `_categoryServices.UpdateCategory` method to update the Category object in the database. The method then returns an `Ok` result with the updated Category object.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## DeleteCategory Endpoint and Method
The `DeleteCategory` endpoint is an HTTP DELETE method that deletes an existing category from the database using the `DeleteCategory` method. This endpoint is accessed by sending a DELETE request to the `/{id}/DeleteCategory` route, where `{id}` is the ID of the category to delete.

### DeleteCategory Endpoint
* **Method**: DELETE
* **URL**: `/{id}/DeleteCategory`
* **Path Params**:
    * `id`: The ID of the category to delete.
* **Response**:
    * A string message indicating that the category was deleted.
    * **Example**:

```
"Deleted"
```
* **Code**: `404 Not Found`: The category with the specified ID was not found.
* **Code**: `200 OK`

### DeleteCategory Method
The `DeleteCategory` method is an asynchronous method that deletes an existing Category object from the database. This method takes an `id` parameter that specifies the ID of the category to delete. The method uses the `_categoryServices.GetCategoryById` method to retrieve the Category object with the specified ID from the database. If no category with the specified ID is found, the method returns a `NotFound` result with a message indicating that no category was found.

If a category with the specified ID is found, the method uses the `_categoryServices.DeleteCategory` method to delete the Category object from the database. The method then returns an `Ok` result with a message indicating that the category was deleted.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 # [CategoryController](#CategoryController)


