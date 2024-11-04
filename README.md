Develop an Order Management API using .NET and SQL Server.

The API should integrate with a SQL Server database and support CRUD operations, data validation, custom business rules, and reporting queries.

Requirements:

Entities and Relationships
Ensure each entity is represented in SQL Server with correct relationships and data constraints.
Customer: Includes fields CustomerID, Name, Email, PhoneNumber, Address.
Order: Fields include OrderID, CustomerID (foreign key), OrderDate, Status (e.g., Pending, Shipped, Completed).
OrderItem: Fields include OrderItemID, OrderID (foreign key), ProductID, Quantity, UnitPrice, and TotalPrice.
Product: Includes ProductID, Name, Description, Price, StockQuantity.

API Functionalities

Customer Management: CRUD operations for Customer with validation.

Order Processing:
Create Order: Allow creating a new order by specifying a customer, list of products, and quantities. Deduct stock quantities and validate that all products have enough stock available.
Order Updates: Allow updating Order status (e.g., mark as shipped, completed).
Calculate Total Price: Calculate TotalPrice for each order item and the full order automatically.
Cancel Order: Only allow cancellations for orders in Pending status, and restock the products.

Reporting
Daily Orders Summary: Add a read-only endpoint to retrieve total orders, total revenue, and number of completed orders for a specific date range.
Top Customers: Retrieve a list of top customers based on total spending within a specific timeframe.
