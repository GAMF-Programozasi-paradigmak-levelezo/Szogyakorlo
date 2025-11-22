# Szógyakorló alkalmazás

 Készítsen egy C# konzol alkalmazást, amely magyar–angol szópárokat tartalmazó szótár segítségével segíti a felhasználót a szavak gyakorlásában. A program két üzemmódot kell biztosítson:

- **Tanító üzemmód (T)**: A felhasználó megad egy magyar szót, és a program kiírja annak angol megfelelõjét, ha az szerepel a szótárban.
- **Kikérdezõ üzemmód (K)**: A program véletlenszerûen kiválaszt egy magyar szót, és a felhasználónak meg kell adnia az angol fordítást. A program visszajelzést ad a válasz helyességérõl.

A szótár adatait a program CSV fájlban tárolja (szoparok.csv). Ha a fájl létezik, a program beolvassa induláskor a szópárokat. Ha nem, létrehoz egy alapértelmezett szótárat, majd elmenti azt a fájlba.