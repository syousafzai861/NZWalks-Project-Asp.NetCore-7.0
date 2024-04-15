This repository hosts a robust ASP.NET Core 7 API solution designed to demonstrate a fully functional CRUD (Create, Read, Update, Delete) framework integrated with comprehensive user authentication and role-based authorization. Leveraging the power of Entity Framework Core, the project offers a solid backend setup suitable for various enterprise-grade applications.

Key Features
CRUD Operations: Complete set of APIs allowing manipulation and management of database entities.
Authentication and Authorization:
Utilizes JWT (JSON Web Tokens) for secure authentication.
Implements role-based access control (RBAC) with predefined roles for 'Admin' and 'User', ensuring that certain functionalities are restricted based on the user's role.
Pagination, Sorting, and Filtering: Enhances performance and user experience by providing mechanisms to sort, paginate, and filter the data.
Image Upload Functionality: Supports uploading and handling images, demonstrating file management within the API.
Role-Specific Functional Access: Certain features are accessible only to users with specific rights, enhancing security and user management.
Repository Pattern with Interfaces: Implements the repository design pattern and interfaces to abstract the data layer, promoting a clean, scalable, and maintainable codebase.
