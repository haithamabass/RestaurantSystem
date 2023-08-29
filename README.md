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
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 ## [CategoryController](#CategoryController)


## GetAll Endpoint and Method
The `GetAll` endpoint is an HTTP GET method that retrieves all available menu items from the database using the `GetAll` method. This endpoint is accessed by sending a GET request to the `/GetAll` route.

### GetAll Endpoint
* **Method**: GET
* **URL**: `/GetAll`
* **Response**:
    * A JSON array of objects representing the available menu items, with each object having the following properties:
        * `DishId`: The ID of the dish.
        * `DishName`: The name of the dish.
        * `Description`: The description of the dish.
        * `Image`: The image of the dish.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `CategoryName`: The name of the category to which the dish belongs.
        * `Available`: Whether the dish is available.
        * `Price`: The price of the dish.
    * **Example**:

```json
[
    {
        "DishId": "1",
        "DishName": "French Fries",
        "Description": "Golden brown, crispy fries served with ketchup.",
        "Price": 3.99
        "Available": true,
        "CategoryId": 1,
        "CategoryName": "Appetizers",
        "Image": "...",
    },
    {
        "DishId": "2",
        "DishName": "Chicken Wings",
        "Description": "Juicy, grilled chicken wings tossed in your choice of sauce.",
        "Price": 10.99
        "Available": true,
        "CategoryId": 1,
        "CategoryName": "Appetizers",
        "Image": "...",
    }
]
```
* **Code**: `204 No Content`: No menu items were found.
* **Code**: `200 OK`

### GetAll Method
The `GetAll` method is an asynchronous method that retrieves all available menu items from the database. This method first checks if any menu items are cached using the `_cache.GetStringAsync` method. If any cached menu items are found, they are deserialized and returned.

If no cached menu items are found, this method uses the `_context.DishesAndOthers` property to access the DishesAndOthers DbSet and retrieve all available dishes from the database. The `Include` method includes related data, in this case, the Category and DishImage navigation properties. The resulting data is then projected into a list of MenuDto objects using the `Select` method.

The resulting list of MenuDto objects is then cached using the `_cache.SetStringAsync` method with a sliding expiration time of 5 minutes. Finally, this method returns the list of MenuDto objects representing all available menu items.

This method is part of a service class called `MenuServices`, which has dependencies on several other services and classes, including an instance of `AppDbContext`, an instance of `ILogger<MenuServices>`, an instance of `IDistributedCache`, an instance of `IDishImageServices`, and an instance of `ICategoryServices`. These dependencies are injected into the constructor of the `MenuServices` class.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetItemById Endpoint and Methods
The `GetItemById` endpoint is an HTTP GET method that retrieves a menu item by its ID from the database using the `GetByIdDto` method. This endpoint is accessed by sending a GET request to the `/{id}` route, where `{id}` is the ID of the menu item to retrieve.

### GetItemById Endpoint
* **Method**: GET
* **URL**: `/{id}`
* **Path Params**:
    * `id`: The ID of the menu item to retrieve.
* **Response**:
    * A JSON object representing the menu item, with the following properties:
        * `DishId`: The ID of the dish.
        * `DishName`: The name of the dish.
        * `Description`: The description of the dish.
        * `Price`: The price of the dish.
        * `Available`: Whether the dish is available.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `CategoryName`: The name of the category to which the dish belongs.
        * `Image`: The image of the dish.
    * **Example**:

```json
{
    "DishId": "1",
    "DishName": "French Fries",
    "Description": "Golden brown, crispy fries served with ketchup.",
    "Price": 3.99,
    "Available": true,
    "CategoryId": 1,
    "CategoryName": "Appetizers",
    "Image": "...",
}
```
* **Code**: `404 Not Found`: The menu item with the specified ID was not found.
* **Code**: `200 OK`


### GetByIdDto Method
The `GetByIdDto` method is an asynchronous method that retrieves a menu item by its ID from the database. This method takes an `id` parameter that specifies the ID of the menu item to retrieve. This method first checks if a menu item with the specified ID is cached using the `_cache.GetStringAsync` method. If a cached menu item is found, it is deserialized and returned.

If no cached menu item is found, this method uses the `_context.DishesAndOthers` property to access the DishesAndOthers DbSet and retrieve a dish with the specified ID from the database. The `Include` method includes related data, in this case, the Category and DishImage navigation properties. The resulting data is then projected into a MenuDto object using the `Select` method.

The resulting MenuDto object is then cached using the `_cache.SetStringAsync` method with a sliding expiration time of 5 minutes. Finally, this method returns a MenuDto object representing a menu item with the specified ID.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetItemByName Endpoint and Methods
The `GetItemByName` endpoint is an HTTP GET method that retrieves a list of menu items by their name from the database using the `GetByNameDto` method. This endpoint is accessed by sending a GET request to the `/name/{name}` route, where `{name}` is the name of the menu items to retrieve.

### GetItemByName Endpoint
* **Method**: GET
* **URL**: `/name/{name}`
* **Path Params**:
    * `name`: The name of the menu items to retrieve.
* **Response**:
    * A JSON array of objects representing the menu items, with each object having the following properties:
        * `DishId`: The ID of the dish.
        * `DishName`: The name of the dish.
        * `Description`: The description of the dish.
        * `Image`: The image of the dish.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `CategoryName`: The name of the category to which the dish belongs.
        * `Available`: Whether the dish is available.
        * `Price`: The price of the dish.
    * **Example**:

```json
[
    {
        "DishId": "1",
        "DishName": "French Fries",
        "Description": "Golden brown, crispy fries served with ketchup.",
        "Price": 3.99,
        "Available": true,
        "CategoryId": 1,
        "CategoryName": "Appetizers",
        "Image": "...",
    },
    {
        "DishId": "2",
        "DishName": "Chicken Wings",
        "Description": "Juicy, grilled chicken wings tossed in your choice of sauce.",
        "Price": 10.99,
        "Available": true,
        "CategoryId": 1,
        "CategoryName": "Appetizers",
        "Image": "...",
    }
]
```
* **Code**: `404 Not Found`: No menu items with the specified name were found.
* **Code**: `200 OK`

### GetByNameDto Method
The `GetByNameDto` method is an asynchronous method that retrieves a list of menu items by their name from the database. This method takes a `name` parameter that specifies the name of the menu items to retrieve. This method first checks if any menu items with the specified name are cached using the `_cache.GetStringAsync` method. If any cached menu items are found, they are deserialized and returned.

If no cached menu items are found, this method uses the `_context.DishesAndOthers` property to access the DishesAndOthers DbSet and retrieve all dishes with names starting with the specified name from the database. The `Include` method includes related data, in this case, the Category and DishImage navigation properties. The resulting data is then projected into a list of MenuDto objects using the `Select` method.

The resulting list of MenuDto objects is then cached using the `_cache.SetStringAsync` method with a sliding expiration time of 5 minutes. Finally, this method returns a list of MenuDto objects representing all menu items with names starting with the specified name.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetItemsByCategory Endpoint and Methods
The `GetItemsByCategory` endpoint is an HTTP GET method that retrieves a list of menu items by their category ID from the database using the `GetAllByCategory` method. This endpoint is accessed by sending a GET request to the `/categoryId/{categoryId}` route, where `{categoryId}` is the ID of the category to retrieve menu items from.

### GetItemsByCategory Endpoint
* **Method**: GET
* **URL**: `/categoryId/{categoryId}`
* **Path Params**:
    * `categoryId`: The ID of the category to retrieve menu items from.
* **Response**:
    * A JSON array of objects representing the menu items, with each object having the following properties:
        * `DishId`: The ID of the dish.
        * `DishName`: The name of the dish.
        * `Description`: The description of the dish.
        * `Price`: The price of the dish.
        * `Available`: Whether the dish is available.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `CategoryName`: The name of the category to which the dish belongs.
        * `Image`: The image of the dish.
    * **Example**:

```json
[
    {
        "DishId": "1",
        "DishName": "French Fries",
        "Description": "Golden brown, crispy fries served with ketchup.",
        "Price": 3.99,
        "Available": true,
        "CategoryId": 1,
        "CategoryName": "Appetizers",
        "Image": "...",
    },
    {
        "DishId": "2",
        "DishName": "Chicken Wings",
        "Description": "Juicy, grilled chicken wings tossed in your choice of sauce.",
        "Price": 10.99,
        "Available": true,
        "CategoryId": 1,
        "CategoryName": "Appetizers",
        "Image": "...",
    }
]
```
* **Code**: `404 Not Found`: No menu items with the specified category ID were found.
* **Code**: `200 OK`

### GetAllByCategory Method
The `GetAllByCategory` method is an asynchronous method that retrieves a list of menu items by their category ID from the database. This method takes an `id` parameter that specifies the ID of the category to retrieve menu items from. This method first checks if any menu items with the specified category ID are cached using the `_cache.GetStringAsync` method. If any cached menu items are found, they are deserialized and returned.

If no cached menu items are found, this method uses the `_context.DishesAndOthers` property to access the DishesAndOthers DbSet and retrieve all dishes with a CategoryId matching the specified category ID from the database. The `Include` method includes related data, in this case, the Category and DishImage navigation properties. The resulting data is then projected into a list of MenuDto objects using the `Select` method.

The resulting list of MenuDto objects is then cached using the `_cache.SetStringAsync` method with a sliding expiration time of 5 minutes. Finally, this method returns a list of MenuDto objects representing all menu items with a CategoryId matching the specified category ID.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## AddItemToMenu Endpoint and Methods
The `AddItemToMenu` endpoint is an HTTP POST method that adds a new menu item to the database using the `CreateDish` method. This endpoint is accessed by sending a POST request to the `/` route with query parameters representing the menu item to add.

### AddItemToMenu Endpoint
* **Method**: POST
* **URL**: `/`
* **Query Params**:
    * A JSON object representing the menu item to add, with the following properties:
        * `DishName`: The name of the dish.
        * `Available`: Whether the dish is available.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `Price`: The price of the dish.
        * `Description`: The description of the dish.
        * `Image`: The image of the dish.
    * **Example**:

```json
{
    "DishName": "French Fries",
    "Available": true,
    "CategoryId": 1,
    "Price": 3.99,
    "Description": "Golden brown, crispy fries served with ketchup.",
    "Image": "..."
}
```
* **Response**:
    * A JSON object representing the added menu item, with the following properties:
        * `DishId`: The ID of the dish.
        * `DishName`: The name of the dish.
        * `Description`: The description of the dish.
        * `Image`: The image of the dish.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `CategoryName`: The name of the category to which the dish belongs.
        * `Available`: Whether the dish is available.
        * `Price`: The price of the dish.
    * **Example**:

```json
{
    "DishId": "1",
    "DishName": "French Fries",
    "Description": "Golden brown, crispy fries served with ketchup.",
    "Image": "...",
    "CategoryId": 1,
    "CategoryName": "Appetizers",
    "Available": true,
    "Price": 3.99
}
```
* **Code**: `400 Bad Request`: A menu item with the same name already exists in the database.
* **Code**: `200 OK`

### ItemIsExist Method
The `ItemIsExist` method is an asynchronous method that checks if a menu item with a specified name already exists in the database. This method takes a `name` parameter that specifies the name of the menu item to check for. This method uses the `_context.DishesAndOthers.AnyAsync` method to check if any dishes in the DishesAndOthers DbSet have a DishName matching the specified name. If any dishes are found, this method returns true, indicating that a menu item with the specified name already exists in the database. Otherwise, this method returns false.

### AddMenuItemToDatabase Method
The `AddMenuItemToDatabase` method is an asynchronous method that adds a new Menu object to the database. This method takes a `model` parameter that specifies a Menu object representing a new menu item to add to the database. This method uses the `_context.DishesAndOthers.AddAsync` and `_context.SaveChangesAsync` methods to add a new Menu object to the DishesAndOthers DbSet and save changes to the database. Finally, this method returns a Menu object representing a newly added menu item.

### CreateDish Method
The `CreateDish` method is an asynchronous method that creates and adds a new menu item to the database. This method takes a `model` parameter that specifies a MenuDtoToAdd object representing a new menu item to add to the database. This method first checks if a category with an ID matching CategoryId from model exists in database using `_categoryServices.GetCategoryById`. If no category is found, this method throws an exception.

If category is found, this method creates a new Menu object using data from model and adds it to database using `_menuServices.AddMenuItemToDatabase`. Then this method adds image for newly created dish using `_dishImageServices.AddDishImage`. After that this method invalidates cache for GetAll endpoint using `_cache.RemoveAsync`. Finally, this method retrieves newly created dish from database using `_menuServices.GetByIdDto` and returns it.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## UpdateDish Endpoint and Methods
The `UpdateDish` endpoint is an HTTP PUT method that updates an existing menu item in the database using the `UpdateDishInMenu` method. This endpoint is accessed by sending a PUT request to the `/UpdateDish` route with query parameters representing the ID of the menu item to update and the updated data for the menu item.

### UpdateDish Endpoint
* **Method**: PUT
* **URL**: `/UpdateDish`
* **Query Params**:
    * `id`: The ID of the menu item to update.
    * A JSON object representing the updated data for the menu item, with the following properties:
        * `DishName`: The updated name of the dish.
        * `Available`: The updated availability of the dish.
        * `CategoryId`: The updated ID of the category to which the dish belongs.
        * `Price`: The updated price of the dish.
        * `Description`: The updated description of the dish.
        * `Image`: The updated image of the dish.
    * **Example**:

```json
{
    "id": "1",
    "DishName": "French Fries",
    "Available": true,
    "CategoryId": 1,
    "Price": 3.99,
    "Description": "Golden brown, crispy fries served with ketchup.",
    "Image": "..."
}
```
* **Response**:
    * A JSON object representing the updated menu item, with the following properties:
        * `DishId`: The ID of the dish.
        * `DishName`: The name of the dish.
        * `Description`: The description of the dish.
        * `Image`: The image of the dish.
        * `CategoryId`: The ID of the category to which the dish belongs.
        * `CategoryName`: The name of the category to which the dish belongs.
        * `Available`: Whether the dish is available.
        * `Price`: The price of the dish.
    * **Example**:

```json
{
    "DishId": "1",
    "DishName": "French Fries",
    "Description": "Golden brown, crispy fries served with ketchup.",
    "Image": "...",
    "CategoryId": 1,
    "CategoryName": "Appetizers",
    "Available": true,
    "Price": 3.99
}
```
* **Code**: `400 Bad Request`: No menu item with specified ID was found in database.
* **Code**: `200 OK`

### UpdateDishInDatabase Method
The `UpdateDishInDatabase` method is a method that updates an existing `Menu` object in database. This method takes a `model` parameter that specifies a `Menu` object representing an existing menu item to update in database. This method uses `_context.Update` and `_context.SaveChanges` methods to update existing Menu object in DishesAndOthers DbSet and save changes to database. Finally, this method returns a Menu object representing an updated menu item.

### UpdateDishInMenu Method
The `UpdateDishInMenu` method is an asynchronous method that updates an existing menu item in database. This method takes two parameters: a Menu object representing an existing menu item to update and a `MenuToUpdateDto` object representing updated data for menu item. This method first checks if passed Menu object is null. If it is null, this method throws an exception.

If passed Menu object is not null, this method updates its properties using data from passed `MenuToUpdateDto` object. Then this method updates image for existing dish using `_dishImageServices.UpdateDishImage`. After that this method updates existing dish in database using `_menuServices.UpdateDishInDatabase`. Then this method invalidates cache for GetAll endpoint using `_cache.RemoveAsync`. Finally, this method retrieves updated dish from database using `_menuServices.GetByIdDto` and returns it.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## DeleteItemfromMenu Endpoint and Methods
The `DeleteItemfromMenu` endpoint is an HTTP DELETE method that deletes an existing menu item from the database using the `DeleteDish` method. This endpoint is accessed by sending a DELETE request to the `/{id}` route, where `{id}` is the ID of the menu item to delete.

### DeleteItemfromMenu Endpoint
* **Method**: DELETE
* **URL**: `/{id}`
* **Path Params**:
    * `id`: The ID of the menu item to delete.
* **Response**:
    * A string indicating that the menu item was deleted.
    * **Example**:

```json
"Deleted"
```
* **Code**: `404 Not Found`: No menu item with specified ID was found in database.
* **Code**: `200 OK`

### DeleteDish Method
The `DeleteDish` method is an asynchronous method that deletes an existing menu item from database. This method takes a Menu object representing an existing menu item to delete from database. This method uses `_context.DishesAndOthers.Remove` and `_context.SaveChanges` methods to remove existing Menu object from DishesAndOthers DbSet and save changes to database. Then this method invalidates cache for `GetAll` and `GetById` endpoints using `_cache.RemoveAsync`. Finally, this method returns a Menu object representing a deleted menu item.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 ## [OrderController](#OrderController)



## GetOrdersForKitchen Endpoint and Method
The `GetOrdersForKitchen` endpoint is an HTTP GET method that retrieves all orders from the database using the `GetAllOrdersForKitchen` method. This endpoint is accessed by sending a GET request to the `/Receive orders` route.

### GetOrdersForKitchen Endpoint
* **Method**: GET
* **URL**: `/Receive orders`
* **Response**:
    * A JSON array of objects representing the orders, with each object having the following properties:
        * `QueuePosition`: The position of the order in the queue.
        * `OrderId`: The ID of the order.
        * `OrderCode`: The code of the order.
        * `OrderDate`: The date of the order.
        * `OrderItems`: A list of objects representing the items in the order, with each object having the following properties:
            * `ItemId`: The ID of the item in the order.
            * `DishName`: The name of the dish in the item in the order.
            * `Quantity`: The quantity of the item in the order.
            * `<EUGPSCoordinates>`: The notes for the item in the order<PhoneNumber>r or "No notes" if there are no notes for this item in the order<PhoneNumber>r.<PhoneNumber>
    * **Example**:

```json
[
    {
        "QueuePosition": 1,
        "OrderId": "1",
        "OrderCode": "OC-A1B2C3D4",
        "OrderDate": "2023-01-01T00:00:00",
        "OrderItems": [
            {
                "ItemId": "1",
                "DishName": "French Fries",
                "Quantity": 2,
                "Notes": "No notes"
            },
            ...
        ]
    },
    ...
]
```
* **Code**: `204 No Content`: No orders were found in database.
* **Code**: `500 Internal Server Error`
* **Code**: `200 OK`

### GetAllOrdersForKitchen Method
The `GetAllOrdersForKitchen` method is an asynchronous method that retrieves all orders from database. This method uses `_context.OrderQueue` property to access OrderQueue DbSet and retrieve all orders from database. Then this method sorts retrieved orders by their position in queue using `.OrderBy(oq => oq.QueuePosition)` and projects them into list of OrderQueueDto objects using `.Select(oq => new OrderQueueDto {...})`. Finally, this method returns list of OrderQueueDto objects representing all orders from database sorted by their position in queue.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetOrders Endpoint and Method
The `GetOrders` endpoint is an HTTP GET method that retrieves all orders from the database using the `GetAllOrders` method. This endpoint is accessed by sending a GET request to the `/GetOrders` route.

### GetOrders Endpoint
* **Method**: GET
* **URL**: `/GetOrders`
* **Response**:
    * A JSON array of objects representing the orders, with each object having the following properties:
        * `OrderId`: The ID of the order.
        * `OrderDate`: The date of the order.
        * `OrderCode`: The code of the order.
        * `OrderStatusId`: The ID of the status of the order.
        * `OrderStatusName`: The name of the status of the order.
        * `CancelDate`: The date when the order was cancelled or null if the order was not cancelled.
        * `CancelCause`: The cause why the order was cancelled or "N/A" if the order was not cancelled.
        * `OrderTypeId`: The ID of the type of the order.
        * `OrderTypeName`: The name of the type of the order.
        * `FullName`: The full name of a customer who placed an order or "N/A" if type of an order is 3.
        * `PhoneNumber`: The phone number of a customer who placed an order or null if type of an order is 3.
        * `Address`: The address of a customer who placed an order or "N/A" if type of an order is 3.
        * `Items`: A list of objects representing the items in the order, with each object having the following properties:
            * `ItemIdDto`: The ID of the item in the order.
            * `DishId`: The ID of the associated dish.
            * `DishName`: The name of the associated dish.
            * `Description`: The description of the associated dish.
            * `ImageURL`: The URL to image for associated dish.
            * `Price`: The price of associated dish.
            * `Quantity`: The quantity of associated dish in item in an order.
            * `<EUGPSCoordinates>`: Notes for associated dish in item in an orde<PhoneNumber>r or "No notes" if there are no notes for this dish.<PhoneNumber>
            * `<EUGPSCoordinates>`: Total price for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
    * **Example**:

```json
[
    {
        "OrderId": "1",
        "OrderDate": "2023-01-01T00:00:00",
        "OrderCode": "OC-A1B2C3D4",
        "OrderStatusId": 5,
        "OrderStatusName": "Pending",
        "CancelDate": null,
        "CancelCause": "N/A",
        "OrderTypeId": 1,
        "OrderTypeName": "...",
        "FullName": "...",
        "PhoneNumber": 1234567890,
        "Address": "...",
        "Items": [
            {
                "ItemIdDto": "1",
                "DishId": "1",
                "DishName": "...",
                "Description": "...",
                "ImageURL": "...",
                "Price": 3.99,
                "Quantity": 2,
                "ItemNotes": "...",
                "TotalPrice": 7.98
            },
            ...
        ]
    },
    ...
]
```
* **Code**: `204 No Content`: No orders were found in database.
* **Code**: `500 Internal Server Error`
* **Code**: `200 OK`

### GetAllOrders Method
The `GetAllOrders` method is an asynchronous method that retrieves all orders from database. This method uses a LINQ query to join several tables in database, including Orders, OrderItems, DishesAndOthers, OrderStatuses, and OrderTypes tables. Then this method sorts retrieved orders by their date using `.OrderByDescending(o => o.OrderDate)` and projects them into list of `OrderDto` objects using `.Select(o => new OrderDto {...})`. Finally, this method returns list of OrderDto objects representing all orders from database sorted by their date.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetOrdersByStatus Endpoint and Method
The `GetOrdersByStatus` endpoint is an HTTP GET method that retrieves all orders with a specified status from the database using the `GetAllOrdersByOrderStatus` method. This endpoint is accessed by sending a GET request to the `/GetItemsByStatus` route with a query parameter representing the ID of the status of the orders to retrieve.

### GetOrdersByStatus Endpoint
* **Method**: GET
* **URL**: `/GetItemsByStatus`
* **Query Params**:
    * `statusId`: The ID of the status of the orders to retrieve.
* **Response**:
    * A JSON array of objects representing the orders, with each object having the following properties:
        * `OrderId`: The ID of the order.
        * `OrderCode`: The code of the order.
        * `OrderStatusId`: The ID of the status of the order.
        * `OrderStatusName`: The name of the status of the order.
        * `OrderTypeId`: The ID of the type of the order.
        * `OrderTypeName`: The name of the type of the order.
        * `Items`: A list of objects representing the items in the order, with each object having the following properties:
            * `ItemIdDto`: The ID of the item in the order.
            * `DishId`: The ID of the associated dish.
            * `DishName`: The name of the associated dish.
            * `Description`: The description of the associated dish.
            * `Price`: The price of associated dish.
            * `Quantity`: The quantity of associated dish in item in an order.
            * `<EUGPSCoordinates>`: Total price for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
            * `<EUGPSCoordinates>`: Notes for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
    * **Example**:

```json
[
    {
        "OrderId": "1",
        "OrderCode": "OC-A1B2C3D4",
        "OrderStatusId": 5,
        "OrderStatusName": "Pending",
        "OrderTypeId": 1,
        "OrderTypeName": "...",
        "Items": [
            {
                "ItemIdDto": "1",
                "DishId": "1",
                "DishName": "...",
                "Description": "...",
                "Price": 3.99,
                "Quantity": 2,
                "TotalPrice": 7.98,
                "ItemNotes": "..."
            },
            ...
        ]
    },
    ...
]
```
* **Code**: `204 No Content`: No orders with specified status were found in database.
* **Code**: `500 Internal Server Error`
* **Code**: `200 OK`

### GetAllOrdersByOrderStatus Method
The `GetAllOrdersByOrderStatus` method is an asynchronous method that retrieves all orders with a specified status from database. This method takes an integer representing identifier for status of orders to retrieve from database. This method uses a LINQ query to join several tables in database, including OrderItems, Orders, DishesAndOthers, OrderStatuses, and OrderTypes tables. Then this method filters retrieved orders by their status using `.Where(o => o.Status.OrderStatusId == statusId)` and sorts them by their date using `.OrderByDescending(o => o.OrderDate)`. Then this method projects them into list of OrderDto objects using `.Select(o => new OrderDto {...})`. Finally, this method returns list of `OrderDto` objects representing all orders with specified status from database sorted by their date.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetOrdersByType Endpoint and Method
The `GetOrdersByType` endpoint is an HTTP GET method that retrieves all orders with a specified type from the database using the `GetAllOrdersByOrderType` method. This endpoint is accessed by sending a GET request to the `/GetItemsByType` route with a query parameter representing the ID of the type of the orders to retrieve.

### GetOrdersByType Endpoint
* **Method**: GET
* **URL**: `/GetItemsByType`
* **Query Params**:
    * `typeId`: The ID of the type of the orders to retrieve.
* **Response**:
    * A JSON array of objects representing the orders, with each object having the following properties:
        * `OrderId`: The ID of the order.
        * `OrderCode`: The code of the order.
        * `OrderStatusId`: The ID of the status of the order.
        * `OrderStatusName`: The name of the status of the order.
        * `OrderTypeId`: The ID of the type of the order.
        * `OrderTypeName`: The name of the type of the order.
        * `Items`: A list of objects representing the items in the order, with each object having the following properties:
            * `ItemIdDto`: The ID of the item in the order.
            * `DishId`: The ID of the associated dish.
            * `DishName`: The name of the associated dish.
            * `Description`: The description of the associated dish.
            * `Price`: The price of associated dish.
            * `Quantity`: The quantity of associated dish in item in an order.
            * `<EUGPSCoordinates>`: Total price for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
            * `<EUGPSCoordinates>`: Notes for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
    * **Example**:

```json
[
    {
        "OrderId": "1",
        "OrderCode": "OC-A1B2C3D4",
        "OrderStatusId": 5,
        "OrderStatusName": "Pending",
        "OrderTypeId": 1,
        "OrderTypeName": "...",
        "Items": [
            {
                "ItemIdDto": "1",
                "DishId": "1",
                "DishName": "...",
                "Description": "...",
                "Price": 3.99,
                "Quantity": 2,
                "TotalPrice": 7.98,
                "ItemNotes": "..."
            },
            ...
        ]
    },
    ...
]
```
* **Code**: `204 No Content`: No orders with specified type were found in database.
* **Code**: `500 Internal Server Error`
* **Code**: `200 OK`

### GetAllOrdersByOrderType Method
The `GetAllOrdersByOrderType` method is an asynchronous method that retrieves all orders with a specified type from database. This method takes an integer representing identifier for type of orders to retrieve from database. This method uses a LINQ query to join several tables in database, including OrderItems, Orders, DishesAndOthers, OrderStatuses, and OrderTypes tables. Then this method filters retrieved orders by their type using `.Where(t => t.OrderTypeId == typeId)` and sorts them by their date using `.OrderByDescending(o => o.OrderDate)`. Then this method projects them into list of OrderDto objects using `.Select(o => new OrderDto {...})`. Finally, this method returns list of OrderDto objects representing all orders with specified type from database sorted by their date.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## GetOrder Endpoint and Method
The `GetOrder` endpoint is an HTTP GET method that retrieves a specific order from the database using the `GetOrderDto` method. This endpoint is accessed by sending a GET request to the `/GetOrder` route with a query parameter representing the ID of the order to retrieve.

### GetOrder Endpoint
* **Method**: GET
* **URL**: `/GetOrder`
* **Query Params**:
    * `id`: The ID of the order to retrieve.
* **Response**:
    * A JSON object representing the order, with the following properties:
        * `OrderId`: The ID of the order.
        * `OrderCode`: The code of the order.
        * `OrderStatusId`: The ID of the status of the order.
        * `OrderStatusName`: The name of the status of the order.
        * `OrderTypeId`: The ID of the type of the order.
        * `OrderTypeName`: The name of the type of the order.
        * `Items`: A list of objects representing the items in the order, with each object having the following properties:
            * `ItemIdDto`: The ID of the item in the order.
            * `DishId`: The ID of the associated dish.
            * `DishName`: The name of the associated dish.
            * `Description`: The description of the associated dish.
            * `Price`: The price of associated dish.
            * `Quantity`: The quantity of associated dish in item in an order.
            * `<EUGPSCoordinates>`: Total price for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
            * `<EUGPSCoordinates>`: Notes for associated dish in item in an orde<PhoneNumber>r.<PhoneNumber>
    * **Example**:

```json
{
    "OrderId": "1",
    "OrderCode": "OC-A1B2C3D4",
    "OrderStatusId": 5,
    "OrderStatusName": "Pending",
    "OrderTypeId": 1,
    "OrderTypeName": "...",
    "Items": [
        {
            "ItemIdDto": "1",
            "DishId": "1",
            "DishName": "...",
            "Description": "...",
            "Price": 3.99,
            "Quantity": 2,
            "TotalPrice": 7.98,
            "ItemNotes": "..."
        },
        ...
    ]
}
```
* **Code**: `400 Bad Request`: No order with specified ID was found in database.
* **Code**: `500 Internal Server Error`
* **Code**: `200 OK`

### GetOrderDto Method
The `GetOrderDto` method is an asynchronous method that retrieves a specific order from database. This method takes a Guid representing identifier for an order to retrieve from database. This method uses a LINQ query to join several tables in database, including Orders, OrderTypes, OrderStatuses, OrderItems, and DishesAndOthers tables. Then this method filters retrieved orders by their identifier using `.Where(o => o.OrderId == orderid)` and projects them into `OrderWithItemsDto` object using `.Select(o => new OrderWithItemsDto {...})`. Finally, this method returns `OrderWithItemsDto` object representing an order with specified identifier from database.

Is there anything else you would like me to include in this documentation? Let me know if you have any other endpoints or methods you would like me to document. 😊
