# Restaurant System


* [Introduction](#Introduction)
* [Technologies](#Technologies)
* [Database](#database)
* [Models](#models)
* [Controllers](#controllers)

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Introduction
Restaurant system WebAPI app designed specifically for businesses! Our app aims to provide a convenient experience, for both restaurant staff and customers. It does so by showcasing the menu and available dishes taking orders from clients and seamlessly transmitting them to the kitchen for preparation. Once an order is ready the app promptly notifies the customer. Generates an invoice for their convenience.

With our app restaurant staff can effortlessly manage orders while keeping track of their status in time. Customers can easily browse through the menu check availability and conveniently place their orders directly within the app. Orders are then instantly relayed to the kitchen for preparation. As soon as an order is complete a notification is sent to inform the staff that the order is ready, for pickup or delivery. Additionally, our app streamlines the payment process by generating invoices that make it quick and hassle-free. Currently, we support cash payments.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Technologies

* .NET 7 WebApi
* Database: Microsoft SQL server.
* Framework/ library: EntityFramework
* Redis
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Database


![222](https://github.com/haithamabass/RestaurantSystem/assets/38409716/ca358c49-a2d2-4cc1-a503-3cb0b88d7142)



The database contains the following tables:

* `Categories`: This table stores the names of the different categories of dishes.
* `DishesAndOthers`: This table stores the details of the dishes and other items that are sold by the restaurant.
* `Images`: This table stores the images of the dishes.
* `InvoiceItems`: This table stores the items that are included in an invoice.
* `Invoices`: This table stores the invoices that are generated for orders.
* `OrderItems`: This table stores the items that are included in an order.
* `OrderQueue`: This table stores the order queue, which is a list of orders that are waiting to be prepared.
* `Orders`: This table stores the details of each order, such as the order date, order type, order status, and notes.
* `OrderStatuses`: This table stores the different order statuses, such as "Pending", "In Progress", "Ready", and "Completed".
* `OrderTypes`: This table stores the different order types, such as "Takeout", "Delivery", and "Dine-in".
* `PaymentStatuses`: This table stores the different payment statuses, such as "Pending", "Paid", and "Cancelled".

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Models


## Menu

* `DishId`: `int` The ID of the dish.
* `DishName`: `string` The name of the dish.
* `Description`: `string` The description of the dish.
* `Price`: `decimal` The price of the dish.
* `Available`: `boolean` Whether the dish is available.
* `CategoryId`: `int` The ID of the category that the dish belongs to.
* `DishImage`: `byte[]` The image of the dish.

### DishImage


* `Id`: `int` The ID of the image.
* `Image`: `byte[]` The image data.
* `DishId`: `int` The ID of the dish that the image belongs to.

### Category


* `CategoryId`: `int` The ID of the category.
* `Name`: `string` The name of the category.
* `Dishes`: `List[Menu]` A collection of `Menu` objects.

### Order
* `OrderId`: `int` The ID of the order.
* `OrderCode`: `string` The order code.
* `OrderDate`: `datetime` The date of the order.
* `OrderTypeId`: `int` The ID of the order type.
* `OrderStatusId`: `int` The ID of the order status.
* `Notes`: `string` Any notes about the order.
* `OrderItems`: `List[OrderItem]` A collection of `OrderItem` objects.
* `OrderType`: `OrderType` The `OrderType` object for the order.
* `Status`: `OrderStatus` The `OrderStatus` object for the order.
* `FullName`: `string` The full name of the customer.
* `PhoneNumber`: `string` The phone number of the customer.
* `Address`: `string` The address of the customer.
* `CancelDate`: `datetime` The date the order was canceled.
* `CancelCause`: `string` The reason the order was canceled.

### OrderItem

* `ItemId`: `int` The ID of the order item.
* `Quantity`: `int` The quantity of the dish in the order item.
* `ItemNotes`: `string` Any notes about the order item.
* `DishId`: `int` The ID of the dish in the order item.
* `OrderId`: `int` The ID of the order that the order item belongs to.
* `Menu`: `Menu` The `Menu` object that the dish in the order item belongs to.

### OrderQueue

* `QueuePosition`: `int` The position of the order in the queue.
* `OrderId`: `int` The ID of the order.
* `Order`: `Order` The `Order` object for the order.

### OrderStatus

* `OrderStatusId`: `int` The ID of the order status.
* `Name`: `string` The name of the order status.

### OrderType

* `OrderTypeId`: `int` The ID of the order type.
* `Name`: `string` The name of the order type.
* `Orders`: `List[Order]` A collection of `Order` objects.

### Invoice
* `InvoiceId`: `int` The ID of the invoice.
* `InvoiceCode`: `string` The code for the invoice.
* `Date`: `datetime` The date of the invoice.
* `Total`: `decimal` The total amount of the invoice.
* `OrderItems`: `List[InvoiceItem]` A collection of `InvoiceItem` objects for the invoice.
* `OrderId`: `int` The ID of the order that the invoice is for.
* `Order`: `Order

### InvoiceItem
* `InvoiceItemId`: `int` The ID of the invoice item.
* `InvoiceId`: `int` The ID of the invoice that the invoice item belongs to.
* `DishId`: `int` The ID of the dish in the invoice item.
* `Quantity`: `int` The quantity of the dish in the invoice item.
* `Notes`: `string` Notes for the invoice item.
* `Menu`: `Menu` The `Menu` object that the dish in the invoice item belongs to.
* `Invoice`: `Invoice` The `Invoice` object that the invoice item belongs to.


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


 
 # Controllers
 
* [CategoryController](#CategoryController)
* [MenuController](#MenuController)
* [OrderController](#OrderController)
* [InvoiceController](#InvoiceController)


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  # CategoryController

 * [GetAllCategories](#GetAllCategories)
 * [GetCategoryById](#GetCategoryById)
 * [AddCategory](#addcategory)
 * [UpdateCategory](#updatecategory)
 * [DeleteCategory](#deletecategory)


                        



## GetAllCategories Endpoint and Methods
The GetAllCategories endpoint is an HTTP GET method that retrieves all categories from the database using `GetAll` method . This endpoint is accessed by sending a GET request to the /Get All Categories route.


### GetAllCategories 
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


### GetCategoryById


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

### AddCategory
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

### UpdateCategory
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

### DeleteCategory
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
# MenuController
 * [GetAllItems](#GetAllItems)
 * [GetItemById](#GetItemById)
 * [GetItemByName](#GetItemByName)
 * [GetItemsByCategory](#GetItemsByCategory)
 * [AddItemToMenu](#AddItemToMenu)
 * [UpdateDish](#UpdateDish)
 * [DeleteItemfromMenu](#DeleteItemfromMenu)




## GetAll Endpoint and Method
The `GetAll` endpoint is an HTTP GET method that retrieves all available menu items from the database using the `GetAll` method. This endpoint is accessed by sending a GET request to the `/GetAll` route.

### GetAllItems
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

### GetItemById
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

### GetItemByName
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

### GetItemsByCategory
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

### AddItemToMenu 
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

### UpdateDish
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

### DeleteItemfromMenu
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
 ## OrderController
   * [GetOrdersForKitchen](#GetOrdersForKitchen)
   * [GetOrders ](#GetOrders)
   * [GetOrdersByStatus ](#GetOrdersByStatus)
   * [GetOrdersByType ](#GetOrdersByType)
   * [GetOrder ](#GetOrder)
   * [CreateOrder ](#CreateOrder)
   * [GetOrderByOrderCode ](#GetOrderByOrderCode)
   * [PrepareNextOrder ](#PrepareNextOrder)
   * [ServeNextOrder ](#ServeNextOrder)
   * [CancelOrder ](#CancelOrder)
   * [CreateOrder ](#CreateOrder)


## GetOrdersForKitchen Endpoint and Method
The `GetOrdersForKitchen` endpoint is an HTTP GET method that retrieves all orders from the database using the `GetAllOrdersForKitchen` method. This endpoint is accessed by sending a GET request to the `/Receive orders` route.

### GetOrdersForKitchen
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

### GetOrders
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

### GetOrdersByStatus
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

### GetOrdersByType
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

### GetOrder
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

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetOrderByOrderCode Endpoint and Methods
The `GetOrderByOrderCode` endpoint is an HTTP GET method that retrieves an order by its order code from the database using the `GetOrderByOrderCodeDto` method. This endpoint is accessed by sending a GET request to the `/GetOrderByOrderCode` route with a query parameter `code` representing the order code of the order to retrieve.

### GetOrderByOrderCode

* Method: GET
* URL: `/GetOrderByOrderCode?code={code}`
* Params:
    * `code`: The order code of the order to retrieve.
* Response:
    * A JSON object representing the order, with the following properties:
        * `OrderId`: The ID of the order.
        * `OrderDate`: The date of the order.
        * `OrderCode`: The order code of the order.
        * `OrderStatusId`: The ID of the order status.
        * `OrderStatusName`: The name of the order status.
        * `CancelCause`: The cancel cause of the order, if applicable.
        * `CancelDate`: The cancel date of the order, if applicable.
        * `OrderTypeId`: The ID of the order type.
        * `OrderTypeName`: The name of the order type.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `Items`: An array of objects representing the items in the order, each with the following properties:
            * `ItemIdDto`: The ID of the item.
            * `DishId`: The ID of the dish.
            * `DishName`: The name of the dish.
            * `Description`: The description of the dish.
            * `Price`: The price of the dish.
            * `Quantity`: The quantity of the dish in this item.
            * `TotalPrice`: The total price of this item (price x quantity).
            * `ItemNotes`: Notes for this item, if any.

    * Example:

```
{
    "OrderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderCode": "OC-ABC12345",
    "OrderStatusId": 5,
    "OrderStatusName": "Pending",
    "CancelCause": "N/A",
    "CancelDate": null,
    "OrderTypeId": 1,
    "OrderTypeName": "Delivery",
    "FullName": "John Doe",
    "PhoneNumber": 1234567890,
    "Address": "123 Main St",
    "Items": [
        {
            "ItemIdDto": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "Description": "Golden brown, crispy fries served with ketchup.",
            "Price": 3.99,
            "Quantity": 2,
            "TotalPrice": 7.98,
            "ItemNotes": "Extra spicy"
        },
        {
            "ItemIdDto": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "Description": "Juicy, grilled chicken wings tossed in your choice of sauce.",
            "Price": 10.99,
            "Quantity": 1,
            "TotalPrice": 10.99,
            "ItemNotes": No onions"
        }
    ]
}
```
* Code: `400 Bad Request`: No order was found with the specified order code or an error occurred while processing the request.
* Code: `200 OK`

### GetOrderByOrderCodeDto Method
The `GetOrderByOrderCodeDto` method is an asynchronous method that takes an order code as input and returns an object representing an order with its associated data. This method uses a LINQ query to join several tables in the database and retrieve information about an order with the specified order code.

If no order is found with the specified order code, an exception is thrown with a message indicating that no order was found. If an exception occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using GetOrderByOrderCodeDto method and rethrown.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


## CreateOrder Endpoint and Methods
The `CreateOrder` endpoint is an HTTP POST method that creates a new order using the `CreateOrder` method. This endpoint is accessed by sending a POST request to the `/CreateOrder` route with a JSON payload in the request body representing the list of `OrderItemToAddDto` objects to add.

### CreateOrder

* Method: POST
* URL: `/CreateOrder`
* Body:
    * A JSON array of objects representing the order items to add, each with the following properties:
        * `DishId`: The ID of the dish.
        * `Quantity`: The quantity of the dish.
        * `ItemNotes`: Notes for the dish.
        * `FullName`: The full name of the customer.
        * `PhoneNumber`: The phone number of the customer.
        * `Address`: The address of the customer.
        * `OrderTypeId`: The ID of the order type.
     
    * example of how you could use this endpoint:

```
POST /CreateOrder HTTP/1.1
Host: yourdomain.com
Content-Type: application/json

[
    {
        "DishId": 1,
        "Quantity": 2,
        "ItemNotes": "Extra spicy",
        "FullName": "John Doe",
        "PhoneNumber": "1234567890",
        "Address": "123 Main St",
        "OrderTypeId": 1
    },
    {
        "DishId": 2,
        "Quantity": 1,
        "ItemNotes": "No onions",
        "FullName": "John Doe",
        "PhoneNumber": "1234567890",
        "Address": "123 Main St",
        "OrderTypeId": 1
    }
]
```

* Response:
    * A JSON array of objects representing the created order items, each with the following properties:
        * `ItemId`: The ID of the order item.
        * `Quantity`: The quantity of the dish.
        * `ItemNotes`: Notes for the dish.
        * `DishId`: The ID of the dish.
        * `OrderId`: The ID of the order.
    * Example:

```
[
    {
        "ItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "Quantity": 2,
        "ItemNotes": "Extra spicy",
        "DishId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "OrderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    },
    {
        "ItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "Quantity": 1,
        "ItemNotes": "No onions",
        "DishId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "OrderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
]
```
* Code: `400 Bad Request`: The input data is invalid or an error occurred while processing the request.
* Code: `200 OK`

### CreateOrder Method
The `CreateOrder` method is an asynchronous method that takes a list of `OrderItemToAddDto` objects as input and returns a list of `OrderItem` objects. This method performs several validation checks on the input data, such as checking if the input list is empty, if the specified `OrderStatusId` and `OrderTypeId` are valid, and if the specified `DishId` for each order item is valid.

If any of these validation checks fail, an exception is thrown with an appropriate error message. If all validation checks pass, a new `Order` object is created and added to the database, along with all the `OrderItem` objects in the input list. The method also enqueues the new order using the `_orderQueueService.EnqueueOrder` method.

If an exception occurs while executing this method, it is logged using the `_logger.LogError` method with a message indicating that an error occurred while using the `CreateOrder` method, and the exception is rethrown.


## OrderQueueService Methods
The `OrderQueueService` class contains two methods for managing the order queue: `EnqueueOrder` and `DequeueOrder`.

#### EnqueueOrder Method
The `EnqueueOrder` method is an asynchronous method that takes an `Order` object as input and adds it to the order queue. This method finds the next available position in the queue and adds a new `OrderQueue` object to the database with the specified `OrderId`. The method then saves the changes to the database.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## PrepareNextOrder Endpoint and Methods
The `PrepareNextOrder` endpoint is an HTTP PUT method that prepares the next order in the queue using the `PrepareNextOrder` method. This endpoint is accessed by sending a PUT request to the `/Prepare Next Order` route.

### PrepareNextOrder

* Method: PUT
* URL: `/Prepare Next Order`
* Response:
    * A JSON object representing the prepared order, with the following properties:
        * `OrderId`: The ID of the order.
        * `OrderCode`: The order code of the order.
        * `OrderDate`: The date of the order.
        * `OrderItems`: An array of objects representing the items in the order, each with the following properties:
            * `ItemId`: The ID of the item.
            * `DishName`: The name of the dish.
            * `Quantity`: The quantity of the dish in this item.
            * `Notes`: Notes for this item, if any.

    * Example:

```
{
    "OrderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "OrderCode": "OC-ABC12345",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderItems": [
        {
            "ItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "Quantity": 2,
            "Notes": "Extra spicy"
        },
        {
            "ItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "Quantity": 1,
            "Notes": No onions"
        }
    ]
}
```
* Code: `404 Not Found`: No orders were found in the queue or an error occurred while processing the request.
* Code: `200 OK`

### PrepareNextOrder Method
The `PrepareNextOrder` method is an asynchronous method that prepares the next order in the queue and returns an object representing that order. This method dequeues the next order from the queue using `_orderQueueService.DequeueOrder` method, updates its status to 'Prepared' using `UpdateOrderStatusToPrepare` method, saves changes to database using `UpdateOrderInDatabase` method and retrieves information about that order using `GetOrderForKitchen` method.

If no orders are found in the queue or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using PrepareNextOrder method and rethrown.

### DequeueOrder Method
The `DequeueOrder` method is an asynchronous method that removes the first order from the order queue and returns it. This method finds the first `OrderQueue` object in the queue, removes it from the database, and saves the changes. The method then retrieves the corresponding `Order` object from the database, including its related data, and returns it.

If no orders are found in the queue, an exception is thrown with a message indicating that no orders were found.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


## ServeNextOrder Endpoint and Methods
The `ServeNextOrder` endpoint is an HTTP PUT method that serves an order by its order code using the `ServeOrder` method. This endpoint is accessed by sending a PUT request to the `/Serve` route with a query parameter `orderCode` representing the order code of the order to serve.

### ServeNextOrder

* Method: PUT
* URL: `/Serve?orderCode={orderCode}`
* Params:
    * `orderCode`: The order code of the order to serve.
* Response:
    * A JSON object representing the invoice for the served order, with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice and it's by default "unpaid yet "

    * Example:

```
{
    "InvoiceDate": "2023-08-27T13:44:09.000Z",
    "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "InvoiceCode": "IC-ABC12345",
    "OrderCode": "OC-ABC12345",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderTypeName": "Delivery",
    "OrderStatusName": "Served",
    "ReturnDate": null,
    "ReturnCause": null,
    "FullName": "John Doe",
    "PhoneNumber": 1234567890,
    "Address": "123 Main St",
    "OrderItems": [
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "CategoryName": "Appetizers",
            "Quantity": 2,
            "Notes": "Extra spicy",
            "Price": 3.99,
            "TotalPriceO": 7.98
        },
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "CategoryName": "Appetizers",
            "Quantity": 1,
            "Notes": No onions",
            "Price": 10.99,
            "TotalPriceO": 10.99
        }
    ],
    Total: 18.97,
    PaymentStatusName: unpaid yet
}
```
* Code: `404 Not Found`: No orders were found with specified order code or an error occurred while processing request.
* Code: `200 OK`

### ServeNextOrder Method
The `ServeNextOrder` method is an asynchronous method that takes an order code as input, serves an order with that code, creates an invoice for that order using `_invoiceServices.CreateInvoice` method, deletes that order from database using `_orderService.DeleteOrderAsync` method and returns an object representing invoice for that order.

If no orders are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using ServeNextOrder method and rethrown.


### DeleteOrderAsync Method
The `DeleteOrderAsync` method is an asynchronous method that takes an order ID as input and deletes an order with that ID from the database. This method retrieves an order with specified ID using `GetOrder` method, checks if that order is in the queue using `_orderQueueService.GetOrderQueueAsync` method, removes that order from the queue if it is in the queue using `_orderQueueService.RemoveOrderQueue` method to remove it from the queue, removes that order from the database using `_context.Orders.Remove` method and saves changes to database using `_context.SaveChangesAsync` method.

If no orders are found with specified ID or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using DeleteOrderAsync method and rethrown.


### CreateInvoice Method
The `CreateInvoice` method is an asynchronous method that takes an order code as input and creates an invoice for an order with that code. This method performs several steps to create an invoice for the specified order:

1. The method checks if the input order code is null. If it is, an `ArgumentNullException` is thrown with a message indicating that the input is invalid.
2. The method retrieves an order with the specified code using the `_orderServices.GetOrderByOrderCode` method.
3. The method checks if the retrieved order is in a state that allows creating an invoice (i.e., its status is not 'Unprepared' or 'Pending'). If it is not, an `Exception` is thrown with a message indicating that an invoice cannot be created for an unprepared order.
4. The method creates a new `Invoice` object and sets its properties using data from the retrieved order. Specifically, it sets the `OrderId`, `Date`, `Total`, `FullName`, `Address`, and `PhoneNumber` properties of the new invoice object using data from the retrieved order. It also sets the `PaymentStatus` property of the new invoice object to 'Unpaid' and if the status of the retrieved order is 'Canceled' It sets the `PaymentStatus` property of the new invoice object to 'Canceled'.
5. The method adds the new invoice to the database using the `AddInvoiceInDatabase` method.
6. The method sets the `OrderItems` property of the new invoice object using data from the retrieved order. Specifically, it creates a new `InvoiceItem` object for each item in the retrieved order and sets its properties using data from that item.
7. The method saves changes to the database using the `_context.SaveChangesAsync` method.
8. The method retrieves information about the new invoice using the `GetInvoiceByInvoiceCode` method.

If no orders are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using CreateInvoice method and rethrown.


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Here is the technical documentation for the `CancelOrder` method and endpoint:

## CancelOrder Endpoint and Methods
The `CancelOrder` endpoint is an HTTP PUT method that cancels an order using the `UpdateOrderStatusToCancelOrder` method. This endpoint is accessed by sending a PUT request to the `/cancel order` route with a query parameter `order` representing the order to cancel.

### CancelOrder

* Method: PUT
* URL: `/cancel order?order={order}`
* Params:
    * `order`: A JSON object representing the order to cancel, with the following properties:
        * `OrderCode`: The order code of the order to cancel.
        * `ReturnCause`: The reason for canceling the order.
* Response:
    * A JSON object representing the invoice for the canceled order.
    * Example:

```
{
    "InvoiceDate": "2023-08-27T13:44:09.000Z",
    "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "InvoiceCode": "IC-ABC12345",
    "OrderCode": "OC-ABC12345",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderTypeName": "Delivery",
    "OrderStatusName": "Canceled",
    "ReturnDate": null,
    "ReturnCause": null,
    "FullName": "John Doe",
    "PhoneNumber": 1234567890,
    "Address": "123 Main St",
    "OrderItems": [
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "CategoryName": "Appetizers",
            "Quantity": 2,
            "Notes": "Extra spicy",
            "Price": 3.99,
            "TotalPriceO": 7.98
        },
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "CategoryName": "Appetizers",
            "Quantity": 1,
            "Notes": No onions",
            "Price": 10.99,
            "TotalPriceO": 10.99
        }
    ],
    Total: 18.97,
    PaymentStatusName: Canceled
}
```
* Code: `400 Bad Request`: No orders were found with specified order code or an error occurred while processing request.
* Code: `200 OK`

### CancelOrder Method
The `CancelOrder` method is an asynchronous method that takes a `CancelOrderDto` object as input, cancels an order with specified code, creates an invoice for that order using `_invoiceServices.CreateInvoice` method, deletes that order from database using `_orderService.DeleteOrderAsync` method and returns an object representing invoice for that order.

If no orders are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `CancelOrder` method and rethrown.



Here is a more detailed version of the technical documentation for the `CreateInvoice` method:

### CreateInvoice Method
The `CreateInvoice` method is an asynchronous method that takes an order code as input and creates an invoice for an order with that code. This method performs several steps to create an invoice for the specified order:

1. The method checks if the input order code is null. If it is, an `ArgumentNullException` is thrown with a message indicating that the input is invalid.
2. The method retrieves an order with the specified code using the `_orderServices.GetOrderByOrderCode` method.
3. The method checks if the retrieved order is in a state that allows creating an invoice (i.e., its status is not 'Unprepared' or 'Pending'). If it is not, an `Exception` is thrown with a message indicating that an invoice cannot be created for an unprepared order.
4. The method creates a new `Invoice` object and sets its properties using data from the retrieved order. Specifically, it sets the `OrderId`, `Date`, `Total`, `FullName`, `Address`, and `PhoneNumber` properties of the new invoice object using data from the retrieved order. It also sets the `PaymentStatus` property of the new invoice object to 'Unpaid' and if the status of the retrieved order is 'Canceled' It sets the `PaymentStatus` property of the new invoice object to 'Canceled'.
5. The method adds the new invoice to the database using the `AddInvoiceInDatabase` method.
6. The method sets the `OrderItems` property of the new invoice object using data from the retrieved order. Specifically, it creates a new `InvoiceItem` object for each item in the retrieved order and sets its properties using data from that item.
7. The method saves changes to the database using the `_context.SaveChangesAsync` method.
8. The method retrieves information about the new invoice using the `GetInvoiceByInvoiceCode` method.

If no orders are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using CreateInvoice method and rethrown.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 ## [InvoiceController](#InvoiceController)
   * [GetInvoices](#GetInvoices)
   * [GetOrGetReadyToPickUpInvoicesders](#GetReadyToPickUpInvoices)
   * [GetReadyForDeliveryInvoices](#GetReadyForDeliveryInvoices)
   * [GetInvoiceByInvoiceCode](#GetInvoiceByInvoiceCode)
   * [GetInvoicesByOrderType](#GetInvoicesByOrderType)
   * [CreateInvoice](#CreateInvoice)
   * [ProceedInvoices](#ProceedInvoices)
   * [UpdateInvoice](#UpdateInvoice)


## GetInvoices Endpoint and Methods
The `GetInvoices` endpoint is an HTTP GET method that retrieves all invoices from the database using the `GetAllInvoices` method. This endpoint is accessed by sending a GET request to the `/GetInvoices` route.

### GetInvoices

* Method: GET
* URL: `/GetInvoices`
* Response:
    * An array of JSON objects representing all invoices, each with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
[
    {
        "InvoiceDate": "2023-08-27T13:44:09.000Z",
        "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "InvoiceCode": "IC-ABC12345",
        "OrderCode": "OC-ABC12345",
        "OrderDate": "2023-08-27T13:44:09.000Z",
        "OrderTypeName": "Delivery",
        "OrderStatusName": "Served",
        "ReturnDate": null,
        "ReturnCause": null,
        "FullName": "John Doe",
        "PhoneNumber": 1234567890,
        "Address": "123 Main St",
        "OrderItems": [
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "French Fries",
                "CategoryName": "Appetizers",
                "Quantity": 2,
                "Notes": "Extra spicy",
                "Price": 3.99,
                "TotalPriceO": 7.98
            },
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "Chicken Wings",
                "CategoryName": "Appetizers",
                "Quantity": 1,
                "Notes": No onions",
                "Price": 10.99,
                "TotalPriceO": 10.99
            }
        ],
        Total: 18.97,
        PaymentStatusName: Paid
    },
    ...
]
```
* Code: `204 No Content`: No invoices were found or an error occurred while processing request.
* Code: `200 OK`

### GetInvoices Method
The `GetInvoices` method is an asynchronous method that retrieves all invoices from database using `_invoiceServices.GetAllInvoices` method and returns a list of objects representing those invoices.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `GetInvoices` method.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetReadyToPickUpInvoices Endpoint and Methods
The `GetReadyToPickUpInvoices` endpoint is an HTTP GET method that retrieves all invoices for orders that are ready for pickup from the database using the `GetAllInvoicesOfOrdersReadyForPickUp` method. This endpoint is accessed by sending a GET request to the `/GetReadyToPickUpInvoices` route.

### GetOrGetReadyToPickUpInvoicesders
* Method: GET
* URL: `/GetReadyToPickUpInvoices`
* Response:
    * An array of JSON objects representing all invoices for orders that are ready for pickup, each with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
[
    {
        "InvoiceDate": "2023-08-27T13:44:09.000Z",
        "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "InvoiceCode": "IC-ABC12345",
        "OrderCode": "OC-ABC12345",
        "OrderDate": "2023-08-27T13:44:09.000Z",
        "OrderTypeName": "Take Away",
        "OrderStatusName": "Ready to Pickup",
        "ReturnDate": null,
        "ReturnCause": null,
        "FullName": "John Doe",
        "PhoneNumber": 1234567890,
        "Address": "123 Main St",
        "OrderItems": [
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "French Fries",
                "CategoryName": "Appetizers",
                "Quantity": 2,
                "Notes": "Extra spicy",
                "Price": 3.99,
                "TotalPriceO": 7.98
            },
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "Chicken Wings",
                "CategoryName": "Appetizers",
                "Quantity": 1,
                "Notes": No onions",
                "Price": 10.99,
                "TotalPriceO": 10.99
            }
        ],
        Total: 18.97,
        PaymentStatusName: Paid
    },
    ...
]
```
* Code: `204 No Content`: No invoices were found or an error occurred while processing request.
* Code: `200 OK`

### GetReadyToPickUpInvoices Method
The `GetReadyToPickUpInvoices` method is an asynchronous method that retrieves all invoices for orders that are ready for pickup from database using `_invoiceServices.GetAllInvoicesOfOrdersReadyForPickUp` method and returns a list of objects representing those invoices.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using ` GetReadyToPickUpInvoices` method

Here is a more detailed version of the technical documentation for the `GetAllInvoicesOfOrdersReadyForPickUp` method:

### GetAllInvoicesOfOrdersReadyForPickUp Method
The `GetAllInvoicesOfOrdersReadyForPickUp` method is an asynchronous method that retrieves all invoices for orders that are ready for pickup from the database. This method performs several steps to retrieve all invoices for orders that are ready for pickup:

1. The method queries the database for all invoices where the associated order has a type of 'Take Away' and a status that is not 'Canceled'.
2. The method includes related data for each invoice, such as its order items and their associated menu items.
3. The method projects the result of the query into a list of `InvoiceDto` objects, setting their properties using data from the retrieved invoices and their related data.
4. The method returns the list of `InvoiceDto` objects.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `GetAllInvoicesOfOrdersReadyForPickUp` method and rethrown.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetReadyForDeliveryInvoices Endpoint and Methods
The `GetReadyForDeliveryInvoices` endpoint is an HTTP GET method that retrieves all invoices for orders that are ready for delivery from the database using the `GetAllInvoicesOfOrdersReadyForDelivery` method. This endpoint is accessed by sending a GET request to the `/GetReadyForDeliveryInvoices` route.

### GetReadyForDeliveryInvoices

* Method: GET
* URL: `/GetReadyForDeliveryInvoices`
* Response:
    * An array of JSON objects representing all invoices for orders that are ready for delivery, each with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
[
    {
        "InvoiceDate": "2023-08-27T13:44:09.000Z",
        "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "InvoiceCode": "IC-ABC12345",
        "OrderCode": "OC-ABC12345",
        "OrderDate": "2023-08-27T13:44:09.000Z",
        "OrderTypeName": "Delivery",
        "OrderStatusName": "Ready to Deliver",
        "ReturnDate": null,
        "ReturnCause": null,
        "FullName": "John Doe",
        "PhoneNumber": 1234567890,
        "Address": "123 Main St",
        "OrderItems": [
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "French Fries",
                "CategoryName": "Appetizers",
                "Quantity": 2,
                "Notes": "Extra spicy",
                "Price": 3.99,
                "TotalPriceO": 7.98
            },
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "Chicken Wings",
                "CategoryName": "Appetizers",
                "Quantity": 1,
                "Notes": No onions",
                "Price": 10.99,
                "TotalPriceO": 10.99
            }
        ],
        Total: 18.97,
        PaymentStatusName: Paid
    },
    ...
]
```
* Code: `204 No Content`: No invoices were found or an error occurred while processing request.
* Code: `200 OK`

### GetReadyForDeliveryInvoices Method
The `GetReadyForDeliveryInvoices` method is an asynchronous method that retrieves all invoices for orders that are ready for delivery from database using `_invoiceServices.GetAllInvoicesOfOrdersReadyForDelivery` method and returns a list of objects representing those invoices.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `GetReadyForDeliveryInvoices` method.


### GetAllInvoicesOfOrdersReadyForDelivery Method
The `GetAllInvoicesOfOrdersReadyForDelivery` method is an asynchronous method that retrieves all invoices for orders that are ready for delivery from the database. This method performs several steps to retrieve all invoices for orders that are ready for delivery:

1. The method queries the database for all invoices where the associated order has a type of 'Delivery' and a status that is not 'Canceled'.
2. The method includes related data for each invoice, such as its order items and their associated menu items.
3. The method projects the result of the query into a list of `InvoiceDto` objects, setting their properties using data from the retrieved invoices and their related data. Specifically, it sets the `InvoiceDate`, `InvoiceId`, `InvoiceCode`, `OrderCode`, `OrderDate`, `OrderTypeName`, `OrderStatusName`, `ReturnCause`, `ReturnDate`, `FullName`, `PhoneNumber`, `Address`, `Total`, and `PaymentStatusName` properties of each `InvoiceDto` object using data from the retrieved invoice and its related data. It also sets the `OrderItems` property of each `InvoiceDto` object to a list of `InvoiceItemDto` objects, setting their properties using data from the retrieved invoice items and their related data.
4. The method returns the list of `InvoiceDto` objects.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `GetAllInvoicesOfOrdersReadyForDelivery` method.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## GetInvoiceByInvoiceCode Endpoint and Methods
The `GetInvoiceByInvoiceCode` endpoint is an HTTP GET method that retrieves an invoice by its invoice code from the database using the `GetInvoiceByInvoiceCode` method. This endpoint is accessed by sending a GET request to the `/GetInvoiceByInvoiceCode` route with a query parameter `invoiceCode` specifying the invoice code of the invoice to retrieve.

### GetInvoiceByInvoiceCode
* **Method**: GET
* **URL**: `/GetInvoiceByInvoiceCode`
* **Query Params**:
    * `invoiceCode`: The invoice code of the invoice.
* **Response**:
    * A JSON object representing the invoice, with the following properties:
        * `invoiceDate`: The date of the invoice.
        * `invoiceId`: The ID of the invoice.
        * `invoiceCode`: The code of the invoice.
        * `orderCode`: The code of the order associated with the invoice.
        * `orderDate`: The date of the order associated with the invoice.
        * `orderTypeName`: The name of the order type associated with the order.
        * `orderStatusName`: The name of the order status associated with the order.
        * `returnDate`: The date of return, if applicable.
        * `returnCause`: The cause of return, if applicable.
        * `fullName`: The full name of the customer associated with the order.
        * `phoneNumber`: The phone number of the customer associated with the order.
        * `address`: The address of the customer associated with the order.
        * `total`: The total amount of the invoice.
        * `orderItems`: An array of items in the order, each represented as a JSON object with the following properties:
            * `invoiceItemId`: The ID of the invoice item.
            * `dishName`: The name of the dish associated with the invoice item.
            * `categoryName`: The name of the category associated with the dish.
            * `quantity`: The quantity of dishes ordered in this invoice item.
            * `notes`: Any notes associated with this invoice item.
            * `price`: The price of this dish per unit.
            * `totalPriceOfItem`: The total price for this dish in this invoice item (quantity x price).
        * Example:

```json
{
    "invoiceDate": "2023-08-31T16:21:40",
    "invoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "invoiceCode": "INV001",
    "orderCode": "ORD001",
    "orderDate": "2023-08-31T16:21:40",
    "orderTypeName": "Delivery",
    "orderStatusName": "Completed",
    "returnDate": null,
    "returnCause": null,
    "fullName": "John Doe",
    "phoneNumber": 1234567890,
    "address": "123 Main St, Anytown USA",
    "total": 25.98,
    "orderItems": [
        {
            "invoiceItemId": 1,
            "dishName": "French Fries",
            "categoryName": "Appetizers",
            "quantity": 2,
            "notes": "",
            "price": 3.99,
            "totalPriceOfItem": 7.98
        },
        {
            "invoiceItemId": 2,
            "dishName": "Cheeseburger",
            "categoryName": "Entrees",
            "quantity": 1,
            "notes": "",
            "price": 9.99,
            "<EUGPSCoordinates>totalPriceOfItem</EUGPSCoordinates>": 9.99
        }
    ],
    "<EUGPSCoordinates>paymentStatusName</EUGPSCoordinates>":"Paid"
}
```
* **Code**: `204 No Content`: No content was found for this request (i.e., no invoices were found with this invoice code).
* **Code**: `200 OK`
* **Code**: `500 Internal Server Error`: An error occurred while processing this request.

### GetInvoiceByInvoiceCode Method
The `GetInvoiceByInvoiceCode` method is an asynchronous method that returns an InvoiceDto object representing an invoice. This method takes a string parameter named `code` that specifies the code of the invoice to retrieve. If no invoices are found with this code, an exception is thrown indicating that no invoices were found.

The method uses LINQ to query for invoices in `_context.Invoices` where their InvoiceCode property matches the provided code. It includes related data such as OrderItems, Menu, and Category using the `Include` and `ThenInclude` methods. The query is projected into an InvoiceDto object using the `Select` method, and the `FirstOrDefaultAsync` method is used to asynchronously execute the query and retrieve the first result or a default value if no results are found.

If an exception occurs while executing this method, it is logged using the `_logger.LogError` method and rethrown. The error message includes the text “An error occurred while using `GetInvoiceByInvoiceCode` method”.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## GetInvoicesByOrderType Endpoint and Methods
The `GetInvoicesByOrderType` endpoint is an HTTP GET method that retrieves all invoices for orders of a specified type from the database using the `GetAllInvoicesByOrderType` method. This endpoint is accessed by sending a GET request to the `/GetInvoiceByOrderType` route with a query parameter `type` representing the ID of the order type.

### GetInvoicesByOrderType

* Method: GET
* URL: `/GetInvoiceByOrderType?type={type}`
* Params:
    * `type`: The ID of the order type to retrieve invoices for.
* Response:
    * An array of JSON objects representing all invoices for orders of the specified type, each with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
[
    {
        "InvoiceDate": "2023-08-27T13:44:09.000Z",
        "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "InvoiceCode": "IC-ABC12345",
        "OrderCode": "OC-ABC12345",
        "OrderDate": "2023-08-27T13:44:09.000Z",
        "OrderTypeName": "Delivery",
        "OrderStatusName": "Ready to Deliver",
        "ReturnDate": null,
        "ReturnCause": null,
        "FullName": "John Doe",
        "PhoneNumber": 1234567890,
        "Address": "123 Main St",
        "OrderItems": [
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "French Fries",
                "CategoryName": "Appetizers",
                "Quantity": 2,
                "Notes": "Extra spicy",
                "Price": 3.99,
                "TotalPriceO": 7.98
            },
            {
                "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "DishName": "Chicken Wings",
                "CategoryName": "Appetizers",
                "Quantity": 1,
                "Notes": No onions",
                "Price": 10.99,
                "TotalPriceO": 10.99
            }
        ],
        Total: 18.97,
        PaymentStatusName: Paid
    },
    ...
]
```
* Code: `204 No Content`: No invoices were found or an error occurred while processing request.
* Code: `200 OK`

### GetInvoicesByOrderType Method
The `GetInvoicesByOrderType` method is an asynchronous method that retrieves all invoices for orders of a specified type from database using `_invoiceServices.GetAllInvoicesByOrderType` method and returns a list of objects representing those invoices.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `GetInvoicesByOrderType` method and rethrown.

Here is a more detailed version of the technical documentation for the `GetAllInvoicesByOrderType` method:

### GetAllInvoicesByOrderType Method
The `GetAllInvoicesByOrderType` method is an asynchronous method that retrieves all invoices for orders of a specified type from the database. This method performs several steps to retrieve all invoices for orders of the specified type:

1. The method queries the database for all invoices where the associated order has a type that matches the specified type and a status that is not 'Canceled'.
2. The method includes related data for each invoice, such as its order items and their associated menu items.
3. The method projects the result of the query into a list of `InvoiceDto` objects, setting their properties using data from the retrieved invoices and their related data. Specifically, it sets the `InvoiceDate`, `InvoiceId`, `InvoiceCode`, `OrderCode`, `OrderDate`, `OrderTypeName`, `OrderStatusName`, `ReturnCause`, `ReturnDate`, `FullName`, `PhoneNumber`, `Address`, `Total`, and `PaymentStatusName` properties of each `InvoiceDto` object using data from the retrieved invoice and its related data. It also sets the `OrderItems` property of each `InvoiceDto` object to a list of `InvoiceItemDto` objects, setting their properties using data from the retrieved invoice items and their related data.
4. The method returns the list of `InvoiceDto` objects.

If no invoices are found or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `GetAllInvoicesByOrderType` method and rethrown.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## CreateInvoice Endpoint and Methods
The `CreateInvoice` endpoint is an HTTP POST method that creates an invoice for an order with a specified order code using the `CreateInvoice` method. This endpoint is accessed by sending a POST request to the `/CreateInvoice` route with a JSON body containing the order code.

### CreateInvoice

* Method: POST
* URL: `/CreateInvoice`
* Body:
    * A JSON object representing the order code to create an invoice for, with the following properties:
        * `orderCode`: The order code of the order to create an invoice for.
* Response:
    * A JSON object representing the created invoice, with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
{
    "InvoiceDate": "2023-08-27T13:44:09.000Z",
    "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "InvoiceCode": "IC-ABC12345",
    "OrderCode": "OC-ABC12345",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderTypeName": "Delivery",
    "OrderStatusName": "Ready to Deliver",
    "ReturnDate": null,
    "ReturnCause": null,
    "FullName": "John Doe",
    "PhoneNumber": 1234567890,
    "Address": "123 Main St",
    "OrderItems": [
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "CategoryName": "Appetizers",
            "Quantity": 2,
            "Notes": "Extra spicy",
            "Price": 3.99,
            "TotalPriceO": 7.98
        },
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "CategoryName": "Appetizers",
            "Quantity": 1,
            "Notes": No onions",
            "Price": 10.99,
            "TotalPriceO": 10.99
        }
    ],
    Total: 18.97,
    PaymentStatusName: Paid
}
```
* Code: `400 Bad Request`: No order code was provided or an error occurred while processing request.
* Code: `201 Created`

### CreateInvoice Method
The `CreateInvoice` method is an asynchronous method that creates an invoice for an order with a specified order code using `_invoiceServices.CreateInvoice` method and returns a JSON object representing that invoice.

If no orders are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `CreateInvoice` method and rethrown.

Here is a more detailed version of the technical documentation for the internal `CreateInvoice` method:

### CreateInvoice Method
The `CreateInvoice` method is an asynchronous method that takes an order code as input and creates an invoice for an order with that code. This method performs several steps to create an invoice for the specified order:

1. The method checks if the input order code is null. If it is, an `ArgumentNullException` is thrown with a message indicating that the input is invalid.
2. The method retrieves an order with the specified code using the `_orderServices.GetOrderByOrderCode` method.
3. The method checks if the retrieved order is in a state that allows creating an invoice (i.e., its status is not 'Unprepared' or 'Pending'). If it is not, an `Exception` is thrown with a message indicating that an invoice cannot be created for an unprepared order.
4. The method creates a new `Invoice` object and sets its properties using data from the retrieved order. Specifically, it sets the `OrderId`, `Date`, `Total`, `FullName`, `Address`, and `PhoneNumber` properties of the new invoice object using data from the retrieved order. It also sets the `PaymentStatus` property of the new invoice object to 'Unpaid' and if the status of the retrieved order is 'Canceled' It sets the `PaymentStatus` property of the new invoice object to 'Canceled'.
5. The method adds the new invoice to the database using the `AddInvoiceInDatabase` method.
6. The method sets the `OrderItems` property of the new invoice object using data from the retrieved order. Specifically, it creates a new `InvoiceItem` object for each item in the retrieved order and sets its properties using data from that item.
7. The method saves changes to the database using the `_context.SaveChangesAsync` method.
8. The method retrieves information about the new invoice using the `GetInvoiceByInvoiceCode` method.

If no orders are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `CreateInvoice` method and rethrown.


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## ProceedInvoices Endpoint and Methods
The `ProceedInvoices` endpoint is an HTTP PUT method that updates the payment status of an invoice with a specified invoice code to 'Paid' using the `UpdateInvoicePaymentStatusToPaid` method. This endpoint is accessed by sending a PUT request to the `/Proceed Invoices` route with a query parameter `code` representing the invoice code.

### ProceedInvoices

* Method: PUT
* URL: `/Proceed Invoices?code={code}`
* Params:
    * `code`: The invoice code of the invoice to update its payment status to 'Paid'.
* Response:
    * A JSON object representing the updated invoice, with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
{
    "InvoiceDate": "2023-08-27T13:44:09.000Z",
    "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "InvoiceCode": "IC-ABC12345",
    "OrderCode": "OC-ABC12345",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderTypeName": "Delivery",
    "OrderStatusName": "Ready to Deliver",
    "ReturnDate": null,
    "ReturnCause": null,
    "FullName": "John Doe",
    "PhoneNumber": 1234567890,
    "Address": "123 Main St",
    "OrderItems": [
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "CategoryName": "Appetizers",
            "Quantity": 2,
            "Notes": "Extra spicy",
            "Price": 3.99,
            "TotalPriceO": 7.98
        },
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "CategoryName": "Appetizers",
            "Quantity": 1,
            "Notes": No onions",
            "Price": 10.99,
            "TotalPriceO": 10.99
        }
    ],
    Total: 18.97,
    PaymentStatusName: Paid
}
```
* Code: `200 OK`

### ProceedInvoices Method
The `ProceedInvoices` method is an asynchronous method that updates the payment status of an invoice with a specified invoice code to 'Paid' using `_invoiceServices.UpdateInvoicePaymentStatusToPaid` method and returns a JSON object representing that updated invoice.

If no invoices are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `ProceedInvoices` method and rethrown.

Here is a more detailed version of the technical documentation for the internal `UpdateInvoicePaymentStatusToPaid` method:

### UpdateInvoicePaymentStatusToPaid Method
The internal `UpdateInvoicePaymentStatusToPaid` method is an asynchronous method that takes an invoice code as input and updates the payment status of an invoice with that code to 'Paid'. This method retrieves an invoice with specified code using `GetInvoiceByInvoiceCodeToUpdate` method, checks if that invoice is in a state that allows updating its payment status, sets its `PaymentStatus` property to 'Paid' or 'Canceled' depending on the status of the associated order, updates that invoice in the database using `UpdateInvoiceInDatabase` method and retrieves information about that updated invoice using `GetInvoiceByInvoiceCode` method.

If no invoices are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `UpdateInvoicePaymentStatusToPaid` method and rethrown.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## UpdateInvoice Endpoint and Methods
The `UpdateInvoice` endpoint is an HTTP PUT method that updates an invoice with a specified invoice code using the `UpdateInvoice` method. This endpoint is accessed by sending a PUT request to the `/Update Invoices` route with a JSON body containing the invoice details to update.

### UpdateInvoice

* Method: PUT
* URL: `/Update Invoices`
* Body:
    * A JSON object representing the invoice details to update, with the following properties:
        * `InvoiceCode`: The invoice code of the invoice to update.
        * `OrderStatusId`: The ID of the new order status for the associated order, if applicable.
        * `CancelCause`: The new cancel cause for the associated order, if applicable.
        * `OrderTypeId`: The ID of the new order type for the associated order, if applicable.
        * `FullName`: The new full name of the customer for the associated order, if applicable.
        * `PhoneNumber`: The new phone number of the customer for the associated order, if applicable.
        * `Address`: The new address of the customer for the associated order, if applicable.
* Response:
    * A JSON object representing the updated invoice, with the following properties:
        * `InvoiceDate`: The date of the invoice.
        * `InvoiceId`: The ID of the invoice.
        * `InvoiceCode`: The invoice code of the invoice.
        * `OrderCode`: The order code of the associated order.
        * `OrderDate`: The date of the associated order.
        * `OrderTypeName`: The name of the type of the associated order.
        * `OrderStatusName`: The name of the status of the associated order.
        * `ReturnDate`: The return date of the associated order, if applicable.
        * `ReturnCause`: The return cause of the associated order, if applicable.
        * `FullName`: The full name of the customer, if applicable.
        * `PhoneNumber`: The phone number of the customer, if applicable.
        * `Address`: The address of the customer, if applicable.
        * `OrderItems`: An array of objects representing the items in the associated order, each with the following properties:
            * `InvoiceItemId`: The ID of the item in this invoice.
            * `DishName`: The name of the dish in this item.
            * `CategoryName`: The name of the category of this dish in this item.
            * `Quantity`: The quantity of this dish in this item.
            * `Notes`: Notes for this item, if any.
            * `Price`: The price per unit for this dish in this item.
            * `TotalPriceO`: The total price for this item (price x quantity).
        * `Total`: The total price for all items in this invoice.
        * `PaymentStatusName`: The name of payment status for this invoice.

    * Example:

```
{
    "InvoiceDate": "2023-08-27T13:44:09.000Z",
    "InvoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "InvoiceCode": "IC-ABC12345",
    "OrderCode": "OC-ABC12345",
    "OrderDate": "2023-08-27T13:44:09.000Z",
    "OrderTypeName": "Delivery",
    "OrderStatusName": "Ready to Deliver",
    "ReturnDate": null,
    "ReturnCause": null,
    "FullName": "John Doe",
    "PhoneNumber": 1234567890,
    "Address": "123 Main St",
    "OrderItems": [
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "French Fries",
            "CategoryName": "Appetizers",
            "Quantity": 2,
            "Notes": "Extra spicy",
            "Price": 3.99,
            "TotalPriceO": 7.98
        },
        {
            "InvoiceItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "DishName": "Chicken Wings",
            "CategoryName": "Appetizers",
            "Quantity": 1,
            "Notes": No onions",
            "Price": 10.99,
            <EUGPSCoordinates>10.99
        }
    ],
    Total: 18.97,
    PaymentStatusName: Paid
}
```
* Code: `200 OK`

### UpdateInvoice Method
The internal `UpdateInvoice` method is an asynchronous method that takes an instance of an object representing an invoice to update as input and updates that invoice using `_invoiceServices.UpdateInvoice` method. This method retrieves an invoice with specified code using `GetInvoiceByInvoiceCodeToUpdate` method, checks if that invoice is in a state that allows updating, sets its properties using data from the input object, updates that invoice in the database using `UpdateInvoiceInDatabase` method and retrieves information about that updated invoice using `GetInvoiceByInvoiceCode` method.

If no invoices are found with specified code or an error occurs while executing this method, it is logged using `_logger.LogError` method with a message indicating that an error occurred while using `UpdateInvoice` method and rethrown.

