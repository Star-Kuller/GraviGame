# Техническая документация к игре Gravi
## Введение
Gravi - Это игра головоломка на логику и реакцию про управление гравитацией. <br>
В игре предстоить доставить куб(-ы) игрока к выходу(-ам) используя изменения гравитации. <br>
Этому будут препятствовать подвижные стены и убивающие блоки. <br>
(Игра находится в разработке, все названия рабочие)
### Контекст
Собития игры происходят внтури абстрактной компьютерной системы. <br>
Куб(-ы) игрока являются визуализацией данных. <br>
Точки выхода являются визуализацией ` return [data];` в программировании. <br>
## Содержание
- [Введениние](#введение)
  - [Контекст](#контекст)
- [Геймплей](#геймплей)
  - [Терминология](#терминология)
  - [Управление](#управление)
  - [Системы](#системы)
  - [Механники](#механники)
- [Сцены](#сцены)
- [Блоки](#блоки)
  - [Стены](#стены)
  - [Механизмы](#механизмы)
  - [Прочее](#прочее)
- [Уровни](#уровни)
- [Монетизация и продвижение](#монетизация-и-продвижение)
## Геймплей
### Цель
Уровень завершается когда все блоки игрока достигнут блоков выхода. В зависимости от уровня, блоки игрока могут быть привзаны к разным блокам выхода или несколько блоков игрока к одному выходу. <br>
### Управление
Игрок может изменять наравление гравитации свайпом по экрану. <br>
Гравитация изменяется в соответствии с направлением свайпа. <br>
### Системы
Система гравитации - Все объекты подверженные гравитации движутся по стандартныйм законам физики в направлении гравитации. <br>
### Механники
Сигнал - Это связь которая образуется между источником и получателем, позволяющая управлять получателем. (Пример: Кнопка - источник, лазер - получатель.) 
(Не актуально) -> Сигналы делятся на группы по цвету. Цвет определяется по принципу [шкала оттенков]/[кол-во групп]. Блоки с одинаковым сигналом имеют особую метку и связь.<br>
Смерть - Переход в особоесостояние игры, при котором открывается меню проигрыша и игра останавливается.
### Персонажи (Не актуально)
OS - Операционная система, помогает нам пройти игру.<br>
Virus - Вирус, который появляется в конце первой группы уровней, заражает систему, задача игрока его уничтожить решив все головоломки.<br>
## Сцены
### Сцена меню
Эта сцена загружается при запуске игры, в ней находится меню с кнопками.<br/>
Есть кнопки:
- Играть -> ведёт на сцену списка уровней
- Настройки -> открывает поверх окно с настройками языка и миширами громкости
### Сцена списка уровня
На этой сцене находится многостраничный список уровней.
На каждой странице по 8 уровней, всего 10 страниц.
У каждого уровня отображенается рекордное время и превью.
Недоступные уровни отображаются чёрно-белыми.
## Блоки (Нужно полностью переоформить)
Все блоки имеют несколько параметров: <br>
Теги - Ключевые слова опысывающие свойства блока. <br>
Размер - Размер и форма в сетке тайлов игры. <br>
Особености - Особые свойства блока. <br>
Вид - Описание внешнего вида. <br>
### Теги
- На фоне : Другие блоки свободно проходят сквозь него. <br>
- Механизированный : Движется по алгоритму. <br>
- Цикличные : Могут быть только Механизированными, движутся между котрольными точками циклично. <br>
- Падает : Горавитация влияет на этот бок. <br>
- 1-осевой : Движется только вдоль одной оси. <br>
- Свободный : Движется по двум осям. <br>
- Вращается : Способен вращаться. <br>
- Якорь : Этот блок может быть осью вращения или задавать движение для нескольких блоков. <br>
- Убивает : При сопрекосновании происходит смерть. <br>
- Свайп : Прикосновение к экрану с последующим движением. <br>
- Сигнал : Является получателем сигнала. <br>
- Источник : Является источником сигнала. <br>
### Статичные блоки
Эти блоки сохраняют одну и ту же позицию на глобальной сетке.
#### Стена
Теги: Нет <br>
Размер: Квадрат 1x1 <br>
Особености: нет <br>
Вид: Синий не заполненый квадрат неонового цвета, соседние блоки объеденяются и границы между ними нет. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/f2437345-6ab1-4747-8640-4f06852b43af)
### Подвижные блоки
#### Мех-стена
Теги: Нет <br>
Размер: Квадрат 1x1 <br>
Особености: нужна только для обозначения границ механизмов. <br>
Вид: Фиолетовый не заполненый квадрат неонового цвета, соседние блоки объеденяются и границы между ними нет. <br>
#### падающая стена
Теги:  <br>
Размер: Квадрат 1x1 <br>
Особености: Нужен для объединения в единую подвижную конструкцию. <br>
Вид: Квадрат неонового цвета, соседние блоки объеденяются и границы между ними нет. <br>
#### анти-стена
Теги:  <br>
Размер: Квадрат 1x1 <br>
Особености: Нужен для объединения в единую подвижную конструкцию, гравитация работает в обратном направлении. <br>
Вид: Квадрат неонового цвета, соседние блоки объеденяются и границы между ними нет. <br>
#### Ось
Теги:  <br>
Размер: блок 1x1 <br>
Особености: Нет <br>
Вид: С точкой в центре и стрелками указывающими направление вращения. <br>
#### Рельс
Теги:  <br>
Размер: блок 1x1 <br>
Особености: Нет <br>
Вид: Со стрелкой указывающей направление движения, движение может быть ограничено. Есть линия показывающая траекторию.<br>
#### Анти-ось
Теги: Статичный, Твердый, Вращается, Якорь <br>
Размер: блок 1x1 <br>
Особености: Гравитация раотает в оратном направлении. <br>
Вид: С точкой в центре и стрелками указывающими направление вращения. <br>
#### Анти-рельс
Теги: Падает, Твердый, 1-осевой, Якорь <br>
Размер: блок 1x1 <br>
Особености: Гравитация раотает в оратном направлении. <br>
Вид: Со стрелкой указывающей направление движения, движение может быть ограничено. Есть линия показывающая траекторию.<br>
#### Убивающий блок
Теги: Статичный, Твердый, Убивает <br>
Размер: Квадрат 0.5x0.5 <br>
Особености: нет <br>
Вид: Красный не заполненый квадрат неонового цвета с полосами опастности, внешний вид блоки имеет размер 1x1, соседние блоки объеденяются и границы между ними нет. <br>
### Механизмы
#### Лазер
Теги: падает, задний, сигнал <br>
Размер: 1x1 <br>
Особености: Может работать по сигналу или постоянно, испускает убивающий луч.<br>
Вид: <br>
#### Мех-ось
Теги: Статичный, Якорь, Вращается, Сигал <br>
Размер: блок 1x1 <br>
Особености: Может работать по сигналу или постоянно, вращается в указаном направлении.<br>
Вид: Фиолетовый, с точкой в центре и стрелками указывающими направление вращения. <br>
#### Мех-платформа
Теги: Статичный, Твердый, Вращается, Якорь, Свободный, Сигал <br>
Размер: блок 1x1 <br>
Особености: Может работать по сигналу сменять на следующую позицию, по сигналу бесконечно дигаться или бесконечно двигаться, перемещается между масивом точек за указаное время, с указаным временем задежки между перемещениями.<br>
Вид: Фиолетовый, со стрелками указывающими направление движения. Есть линия показывающая траекторию.<br>
#### Кнопка
Теги: Статичный, Задний, Источник <br>
Размер: 1x0.5 <br>
Особености: При соприкосновении с твёрдым объектом испускает сигнал. <br>
Вид: серая прямоугольная пластина с цветной кнопкой по цвету сигнала, в профиль. крепится на стену. <br>
#### Тумблер
Теги: Статичный, Задний, Источник <br>
Размер: 1x0.5 <br>
Особености: Может быть в двух состоянихя Испускает сигнал и не испускает сигнал, При соприкосновении с твёрдым объектом меняет состояние. <br>
Вид: серая прямоугольная пластина с выключателем, в профиль. Крепится на стену. <br>
#### Таймер
Теги: Статичный, Задний, Источник, Сигнал <br>
Размер: 0.5x0.5 <br>
Особености: Может работать по сигналу или постоянно, испускает сигнал в течении N секунд, затем не испускает сигнал в течении R секунд. Работает в цикле. <br>
Вид: Прямоугольник с индикатором времени на таймере. Цвет индикатора соответствует цвету сигнала. <br>
### Прочее
#### блок игрока
Теги: Свободный, Вращается, Падает, Твердый <br>
Размер: Квадрат 1x1 <br>
Особености: См. Финиширование. <br>
Вид: Неоновый квадрат, цвет морской волны - контур, цвет морской волны прозрачность 50% - заливка, внутри надпись `[Data]`. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/a1effb02-c621-4051-b810-1846bd911fd8)
#### блок выхода
Теги: Статичный, Задний <br>
Размер: Квадрат 1x1 <br>
Особености: См. Финиширование. <br>
Вид: Зеленый не заполненый квадрат неонового цвета с узорои и надписью `return;`, внешний вид блоки имеет размер 2x2, имеет цветовую метку. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/b63c8244-415b-4ccf-baab-05f59a115cea)
## Уровни
Будет 80 уровней разбитые на 10 групп по 8 уровней. <br>
### Группа 1
#### Уровень 1
Это обучающий уровень, на нем будет появлятся подсказка рассказывающая об возможностим изменять направление гравитации свайпом по экрану в желаемом направлении. <br>
Представляет из себя Г-образный коридор, где блок-игрока в начала и блок выхода в конце. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/137d26f3-af55-4650-b304-749f94f44290)
#### Уровень 2
Это обучающий уровень, на нём будет рассказано про блоки движущиеся по мимо игрока. <br>
Представляет из себя U-образный коридор где в центре есть дверь подверженная гравитации. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/c35fe81e-b41f-40bc-a8d8-0ebef5944256)
#### Уровень 3
Представляет из себя U-обрызный коридор, рельс перекрывает путь к выходу с точки старта и к финишу, в центре стоит 3 блока закрепленные на оси. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/51b7e762-a4a7-43e7-8686-08848582de50)
#### Уровень 4
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/1bad2b28-0be8-4080-a757-0a51be349356)
#### Уровень 5
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/9b0aadd5-fcc3-4968-ad2c-11667bf4e4ff)
#### Уровень 6
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/dcd3d56b-894d-4ebd-8142-957352111023)
#### Уровень 7
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/eaace82a-43c2-432a-86bb-e539f7e7ecf9)
#### Уровень 8
В этом уровне, появляется персонаж Virus, который заразил несколько стен. <br>
![image](https://github.com/Star-Kuller/GraviGame/assets/68467056/7e7c89a4-7f8b-44d7-934d-362318dfdb19)
### Группа 2
### Группа 3
### Группа 4
### Группа 5
### Группа 6
### Группа 7
### Группа 8
### Группа 9
### Группа 10
## Монетизация и продвижение
