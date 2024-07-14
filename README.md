# ChessDotNet
ChessDotNet - это .NET реализация известной шахматной Typescript библиотеки [chess.js](https://github.com/jhlywa/chess.js). Она содержит тот же функционал, что и оригинальная библиотека. Написана на языке C# с применением подходов ООП. Основное её предназначение - анализ и валидация шахматных партий. Библиотека тщательно протестирована. Большая часть тестов взята из исходного репозитория и переписана с Jest'а на xUnit, часть я добавил самостоятельно.
# Установка
Для ознакомления с функционалом можно установить nuget пакет по этой [ссылке](https://www.nuget.org/packages/akaWild.ChessDotNet/1.0.0)
# API
## Пользовательские типы
### PublicData
Содержит 2 статических поля: 
- DefaultChessPosition - стартовая позиция на доске
- Squares - массив полей шахматной доски. Каждое поле представлено типом [ChessSquare](#chesssquare)
### FenValidationResult
Результат валидации партии в формате [FEN](https://ru.wikipedia.org/wiki/%D0%9D%D0%BE%D1%82%D0%B0%D1%86%D0%B8%D1%8F_%D0%A4%D0%BE%D1%80%D1%81%D0%B0%D0%B9%D1%82%D0%B0_%E2%80%94_%D0%AD%D0%B4%D0%B2%D0%B0%D1%80%D0%B4%D1%81%D0%B0). Используется в методе [ValidateFen](#validatefen)
### ChessSquare<a id='chesssquare'></a>
Представляет собой поле шахматной доски. Имеет 2 перегруженных конструктора. Также содержит оператор неявного приведения в строку
### PgnHeader<a id='pgnheader'></a>
Представляет собой заголовок шахматной партии в формате [PGN](https://ru.wikipedia.org/wiki/Portable_Game_Notation)
### MoveInfo<a id='moveinfo'></a>
Структура данных, содержащая информацию о шахматном ходе. Используется в перегруженном методе [Move](#move1)
### CommentInfo<a id='commentinfo'></a>
Структура данных, содержащая комментарий по конкретной шахматной позиции. Используется в методе [GetComments](#getcomments)
### ChessPiece<a id='chesspiece'></a>
Содержит данные по шахматной фигуре: [цвет](#chesscolor) и [тип](#chesspiecetype)
### ChessMove<a id='chessmove'></a>
Содержит разнообразные данные по шахматному ходу:
- Color - [цвет фигуры](#chesscolor), которая выполняла ход
- From - [поле](chesssquare) доски, откуда был совершен ход
- To - [поле](chesssquare) доски, куда был совершен ход
- Piece - [тип фигуры](#chesspiecetype), которая выполняла ход
- Capture - [тип фигуры](#chesspiecetype), которая была "съедена" во время хода. Если не было взятий, то тогда = null
- Promotion - [тип фигуры](#chesspiecetype), которая была выбрана при достижении пешки последней линии доски. Если прохода пешки до края не было, то тогда имеет значение null
- Flags - различные флаги, которые характеризуют текущий ход
  - n - обычный ход
  - c - взятие фигуры
  - b - начальный ход пешки на 2 поля вперед
  - e - взятие пешки на проходе
  - p - продвижение пешки до крайней линии доски
  - k - рокировка по королевскому флангу
  - q - рокировка по ферзевому флангу
- San - нотация хода в формате [SAN](https://en.wikipedia.org/wiki/Algebraic_notation_(chess))
- Lan - нотация хода в формате [LAN](https://en.wikipedia.org/wiki/Algebraic_notation_(chess)#:~:text=In%20long%20algebraic%20notation%2C%20also,%22x%22%2C%20e.g.%20Rd3xd7.)
- Before - описание шахматной позиции до хода в формате FEN
- After - описание шахматной позиции после хода в формате FEN
### CastlingRights<a id='castlingrights'></a>
Содержит данные по возможности рокировки (отдельно по королевскому и ферзевому флангу)
### BoardItem<a id='boarditem'></a>
Содержит информацию по конкретному полю: само [поле](chesssquare), [тип фигуры](#chesspiecetype) и ее [цвет](#chesscolor)
## Chess
Основной класс, который содержит все методы для обработки шахматной информации
### Конструктор
Без параметров загружает начальную партию. Либо партию в формате FEN, которая передается в качестве параметра
### Методы (в алфавитном порядке)
#### ClearBoard<a id='clearboard'></a>
Возвращает параметры доски к исходному положению. По умолчанию заголовки партии не сохраняются, если только явно не передать параметр = true
#### DeleteComment<a id='deletecomment'></a>
Удаляет комментарий по текущей позиции. Если комментарий найден и был удален, то возвращается true, в противном случае false
#### DeleteComments<a id='deletecomments'></a>
Удаляет все комментарии по всем позициям
#### GetAttackers<a id='getattackers'></a>
Возвращает список полей, на которых есть фигуры и которые могут атаковать указанное поле. По умолчанию учитываются цвета фигур, чья очередь совершать ход. Но это поведение можно изменить, указав вторым необязательными параметром цвет фигур
#### GetBoard<a id='getboard'></a>
Возвращает двухмерный массив [BoardItem](#boarditem), который описывает текущее состояние шахматной доски. Если поле пустое, то на его месте возвращается null
#### GetCastlingRights<a id='getcastlingrights'></a>
Возвращает тип [CastlingRights](#castlingrights), который описывает возможности рокировки для указанного цвета фигур
#### GetComment<a id='getcomment'></a>
Возвращает комментарий по текущей позиции. Если комментарий отсутствует, то возвращается null
#### GetComments<a id='getcomments'></a>
Возвращает все комментарии по всем позициям в виде массива [CommentInfo](#commentinfo)
#### GetFen<a id='getfen'></a>
Возвращает информацию по текущей позиции в виде строки формата FEN
#### GetHeaders<a id='getheader'></a>
Возвращает заголовки для PGN формата по текущей партии в виде массива [PgnHeader](#pgnheader)
#### GetHistory<a id='gethistory'></a>
Возвращает все совершенные в партии ходы в виде массива строк
#### GetHistoryVerbose<a id='gethistoryverbose'></a>
Возвращает развернутую историю ходов в виде массива [ChessMove](#chessmove)
#### GetMoveNumber<a id='getmovenumber'></a>
Возвращает число полных ходов в партии
#### GetMoves<a id='getmoves'></a>
По умолчанию возвращает список допустимых ходов для всех фигур на всех клетках поля. Это поведение можно переопределить, указав тип и/или положение фигур, для которых нужно сгенерировать возможные варианты ходов
#### GetMovesVerbose<a id='getmovesverbose'></a>
То же, что и для [GetMoves](#getmoves), только список ходов представлен в развернутом формате [ChessMove](#chessmove)
#### GetPgn<a id='getpgn'></a>
Метод позволяет сгенерировать строку с описанием текущей партии в формате PGN. Также опционально можно указать формат разделителей для строк и максимальное количество строк
#### GetPiece<a id='getpiece'></a>
Возвращает описание фигуры по заданному полю в формате [ChessPiece](#chesspiece), либо null, если на указанном поле отсутсвуют фигуры
#### GetSquareColor<a id='getsquarecolor'></a>
Возвращает [цвет](#chesscolor) указанного поля
#### GetTurn<a id='getturn'></a>
Возвращает [цвет](#chesscolor) фигур, чья очередь сейчас ходить
#### IsAttacked<a id='isattacked'></a>
Возвращает true, если указанное поле находится под ударом любой из фигур указанного цвета
#### IsCheck<a id='ischeck'></a>
Возвращает true, если королю стороны, чей сейчас ход, был поставлен шах
#### IsCheckmate<a id='ischeckmate'></a>
Возвращает true, если королю стороны, чей сейчас ход, был поставлен мат
#### IsDraw<a id='isdraw'></a>
Возвращает true, если была зафиксирована ничейная ситуация. Для этого нужна реализация одного из четырёх условий:
- [Правило 50 ходов без взятия фигур и движения пешек](https://en.wikipedia.org/wiki/Fifty-move_rule#:~:text=The%20fifty%2Dmove%20rule%20in,the%20opponent%20completing%20a%20turn)
- У одной из сторон зафиксирована патовая ситуация и ее король не находится под шахом
- На доске недостаточно фигур для постановки мата
- [Правило повторения 3х ходов](https://en.wikipedia.org/wiki/Threefold_repetition)
#### IsGameOver<a id='isgameover'></a>
Возвращает true, если игра завершена. Это может произойти как из-за мата королю, так и в случае фиксации [ничьей](#isdraw)
#### IsInsufficientMaterial<a id='isinsufficientmaterial'></a>
Возвращает true, если на доске недостаточно фигур, чтобы одна из сторон могла поставить мат. В таком случае автоматически фиксируется ничья
#### IsStalemate<a id='isstalemate'></a>
Возвращает true, если сторона, чей сейчас ход, находится в патовом положении
#### IsThreefoldRepetition<a id='isthreefoldrepetition'></a>
Возвращает true, если текущая позиция на доске повторилась 3 или более раза
#### LoadFen<a id='loadfen'></a>
Загружает информацию о новой партии в формате FEN. Если не удается ее загрузить, то генерируется [FenValidationException](fenvalidationexception). Также можно опционально указать, что нужно пропустить валидацию входной строки на соответствие формату FEN. Последний необязательный параметр отвечает за удаление/сохранение заголовков при загрузке новой партии
#### LoadPgn<a id='loadpgn'></a>
Загружает информацию о партии в формате PGN. Если не удалось распарсить входную строку, то генерируется [InvalidPgnException](#invalidpgnexception). Если один или несколько ходов во входной строке имеют неверный формат, то генерируется [InvalidPgnMoveException](#invalidpgnmoveexception). Дополнительно можно указать режим работы парсера (strict = false по умолчанию). Строгий режим позволяет парсить ходы только в формате SAN
#### Move (вариант 1)<a id='move1'></a>
Метод позволяет совершить ход. Также можно указать режим работы парсера. В строгом режиме ходы допускаются только в формате SAN. Если ход является недопустимым или имеет неверный входной формат, то генерируется [InvalidMoveException](#invalidmoveexception)
#### Move (вариант 2)<a id='move2'></a>
Перегруженный вариант метода Move
#### Perft<a id='perft'></a>
Этот метод используется в основном в целях отладки и мониторинга производительности
#### PutPiece<a id='putpiece'></a>
Позволяет разместить фигуру на шахматной доске. Если результат успешный, то возвращается true. В противном случае, доска остается нетронутой
#### RemoveHeader<a id='removeheader'></a>
Метод удаляет заголовок по заданному ключу
#### RemovePiece<a id='removepiece'></a>
Удаляет и возвращает фигуру по указанному полю. Если на поле не было никаких фигур, то возвращается null
#### Resete<a id='reset'></a>
Загружает начальную позицию на доске
#### SetCastlingRights<a id='setcastlingrights'></a>
Устанавливает возможности рокировки для конкретного цвета фигур
#### SetComment<a id='setcomment'></a>
Устанавливает комментарий на текущую позицию
#### SetHeader<a id='setheader'></a>
Устанавливает заголовок
#### ToAscii<a id='toascii'></a>
Возаращает текстовое представление текущей игровой ситуации в виде фигуры из набора ascii-символов
#### Undo<a id='undo'></a>
Отменяет и возвращает последний сделанный ход. Возвращается null, если история ходов пуста
#### ValidateFen<a id='validatefen'></a>
Статический метод, который проверяет строку на соответствие формату FEN. Возвращает объект типа [FenValidationResult](#fenvalidationresult) с результатом валидации
## Перечисления
### ChessPieceType<a id='chesspiecetype'></a>
Перечисление содержит все возможные типы фигур в шахматах
### ChessColor<a id='chesscolor'></a>
Перечисление содержит возможные цвета фигур (белый и черный)
## Исключения
### FenValidationException<a id='invalidfenexception'></a>
Исключение возникает, когда метод [LoadFen](#loadfen) не может загрузить информацию о шахматной партии в формате FEN
### InvalidChessSquareException<a id='invalidchesssquareexception'></a>
Возникает, когда в конструктор структуры [ChessSquare](#chesssquare) подаются неверные данные либо при попытке неявного приведения [ChessSquare](#chesssquare) к строке, имеющей неверный формат
### InvalidMoveException<a id='invalidmoveexception'></a>
Возникает, когда невозможно совершить указанный ход
### InvalidPgnException<a id='invalidpgnexception'></a>
Возникает, когда не удается загрузить партию в формате PGN
### InvalidPgnMoveException<a id='invalidpgnmoveexception'></a>
Возникает, когда один или несколько ходов в описании партии в формате PGN имеют неверный формат
