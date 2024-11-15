# Тестовый проект с реализацией REST API сервисов

Aspire не доработан - проекты запускать нативно. Инфраструктура находится в папке Deploy и разворачивается стандартно - docker compose -d
Миграции применяются на 3 контекста. Команды для обычного терминала (использовать в корне репозитория):
1) dotnet ef database update -c DbContextPassport -p ./Source/Db/Qel.Ef.DesignTimeUtils -s ./Source/Db/Qel.Ef.DesignTimeUtils
2) dotnet ef database update -c DbContextBlacklist -p ./Source/Db/Qel.Ef.DesignTimeUtils -s ./Source/Db/Qel.Ef.DesignTimeUtils
3) dotnet ef database update -c DbContextMain -p ./Source/Db/Qel.Ef.DesignTimeUtils -s ./Source/Db/Qel.Ef.DesignTimeUtils
