﻿---
Title: Repeater
CodeTask: /resources/030_todolist/30_repeater.dothtml.csx
---

# Repeater

Repeater is a control that renders a collection of items using an item template. To use it, follow these steps:

1. Bind the Repeater's `DataSource` to the collection property. 
2. Set the item template by adding child elements to the `Repeater`. These will be rendered for each item in the collection.
3. DataContext changes inside the item template; you bind to the current item, so double-check your bindings.

Add a `Repeater` to the `<body>` element. For each item in `Items`, it needs to render a `<p>` element with the item's text. To bind to the todo item itself, use `{value: _this}`.