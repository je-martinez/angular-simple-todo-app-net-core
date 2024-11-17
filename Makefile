add-migration:
	dotnet ef migrations add $(NAME) --project simple-todo-database --startup-project simple-todo-api
run-migrations:
	dotnet ef database update --project simple-todo-database --startup-project simple-todo-api