-- 1. Список хобби и количество студентов с этим хобби
SELECT h.name,
       COUNT(m.idHobby) AS Count
  FROM hobby h
       LEFT JOIN
       merge m ON h.idHobby = m.idHobby
 GROUP BY h.name

 -- 2. Самое популярное хобби
 SELECT h.name,
       h.count
  FROM _1_Hobbies h
 WHERE h.count = (
                     SELECT max(count) 
                       FROM _1_Hobbies
                 )

-- 3. Вывод всех студентов, хобби, пол, возраст, группу и куратора
SELECT students.nameSt AS Студент,
       hobby.name AS Хобби,
       students.sex AS Пол,
       students.age AS Возраст,
       groups.nameGr AS Группа,
       curators.nameCur AS Куратор
  FROM hobby
       INNER JOIN
       (
           merge
           INNER JOIN
           (
               students
               INNER JOIN
               (
                   groups
                   INNER JOIN
                   curators ON groups.idCur = curators.id
               ) gc
               ON students.idGroup = gc.id
           ) sgc
           ON merge.idSt = sgc.idSt
       ) msgc
       ON hobby.idHobby = msgc.idHobby;

-- 4. Вывести кураторов и их студентов, отсортировать по убыванию имя куратора, а студента - по возрастанию
SELECT c.nameCur as [Куратор],
       s.nameSt as [Студент]
  FROM curators c
       INNER JOIN
       (
           groups g
           INNER JOIN
           students s ON g.id = s.idGroup
       )
       ON c.id = g.idCur
 GROUP BY c.nameCur,
          s.nameSt
 ORDER BY c.nameCur DESC,
          s.nameSt ASC

-- 5. Средний возраст по группам
SELECT g.nameGr AS Группа,
       avg(s.age) AS [Средний возраст]
  FROM groups g
       INNER JOIN
       students s ON g.id = s.idGroup
 GROUP BY g.nameGr

 -- 6. Вывести студентов и их хобби из определённой группы
 SELECT s.nameSt AS Студент,
       h.name AS Хобби
  FROM groups g
       INNER JOIN
       (
           students s
           INNER JOIN
           (
               merge m
               INNER JOIN
               hobby h ON m.idHobby = h.idHobby
           )
           ON s.idSt = m.idSt
       )
       ON g.id = s.idGroup
 WHERE g.nameGr = 'ПИб-1'
 ORDER BY s.nameSt ASC,
          h.name DESC

-- 7. Вывести кураторов, у которых студенты с возрастом выше или равно среднему по группам
SELECT curators.nameCur
  FROM curators
       LEFT JOIN
       (
           groups
           INNER JOIN
           students ON groups.id = students.idGroup
       )
       ON curators.id = groups.idCur
 WHERE students.age >= (
                           SELECT MAX([Средний возраст]) 
                             FROM _5_AverageAgesOnGroups
                       )
 GROUP BY curators.nameCur

-- 8. Вывести студентов и их хобби, тех, у кого возраст ниже или равно среднему по группам
SELECT s.nameSt, hobby.name
  FROM hobby
       INNER JOIN
       (
           merge
           INNER JOIN
           (
               SELECT *
                 FROM students
                WHERE students.age <= (
                                          SELECT AVG([Средний возраст]) 
                                            FROM _5_AverageAgesOnGroups
                                      )
           ) s
           ON merge.idSt = s.idSt
       )
       ON hobby.idHobby = merge.idHobby