# Blog API

## Overview
We're building a simple Blog using .Net and EFCore, as well as JWT Token for Authentification

## Features

** User Creation and Authentification (Login) ** using JWT Tokens
**Create, Update, Publish, and Unpublish our Blogs**
**Soft Delete Blogs** (This will keep them hidden but still in our system just in case the FBI needs :3)
**Search Blog** (By Different Categories)

## Endpoints
# User

- `Post /User/CreateUser`
- `Post /User/Login`
- `Get /User/GetUserInfo`

# Blog

- `Post /Blog/AddBlog`
- `GET /Blog/GetAllBlogs`
- `PUT /Blog/EditBlog`
- `Get /Blog/GetBlogByUserId/{userId}`
- `DELETE /Blog/DeleteBlog`
- `GET /Blog/GetBlogByCategory/{category}`

## Async Methods in C#

Async methods in C# are used when queries databases without blocking our main threat

This leads to better performances *Allows us to handle more than one request*

