# Gaia Full Stack Development Exercise

## Overview

This project implements a configurable platform for executing operations on two input fields (A and B).

The solution includes both **Part A (Required)** and **Part B (Bonus)** as defined in the assignment.

---

## Technologies

- Server: .NET (C#) Web API
- Client: Angular
- Database: SQL Server
- Architecture: REST API with configurable operation engine

---

## Architecture

### Server
- Contains all business logic
- Exposes REST API
- Loads operations dynamically from configuration file
- Stores operation history in database

### Client
- Basic UI for:
  - Entering value A
  - Selecting an operation
  - Entering value B
  - Viewing result and history data

---

## Dynamic Operation Configuration

Operations are loaded from a configuration file, allowing:

- Adding new operations
- Modifying existing operations
- Removing operations

Without changing source code.

---

## API Endpoints

- `GET /api/operations` – Returns available operations
- `POST /api/calculate` – Executes selected operation

---

## Bonus Implementation

- Each operation is stored in the database
- Returns:
  - Last 3 operations of the same type
  - Monthly count of the same operation type

---
The project was developed using the currently installed local development environment versions.

While the syntax reflects the installed toolchain, I have experience working with newer versions of .NET and Angular and modern language features in production environments.



## Running the Project

### Server