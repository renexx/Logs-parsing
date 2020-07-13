--Uloha A:	Vypiš průměrný počet vstřelených gólů a asistencí za měsíc pro jednotlivé hráče za jednotlivé sezóny--
Select Season.name as season ,Players.name as player, AVG(goals) as goals, AVG(assists) as assists
from PlayerStatistics JOIN Players
ON PlayerStatistics.id_player=Players.id JOIN Season
ON PlayerStatistics.id_season=Season.id
GROUP by Players.name, Season.name
ORDER by Players.name

--ULOHA B:Vypiš hráče, kteří v listopadu 2018 nevstřelili žádnou branku.--
select Players.name, PlayerStatistics.goals from PlayerStatistics JOIN Players
ON PlayerStatistics.id_player=Players.id JOIN Months ON PlayerStatistics.id_month=Months.id
JOIN Season on PlayerStatistics.id_season=Season.id
where goals=0 and Months.name = 'November' and Season.name = '2018'

--Uloha C:Vypiš jména hráčů, kteří jsou v klubu nováčci a neodehráli zatím za klub žádný zápas tzn. nemají ve statistikách zatím žádné údaje.--
select Players.name from Players LEFT JOIN PlayerStatistics
on Players.id=PlayerStatistics.id_player
where PlayerStatistics.id_player IS NULL

--Uloha D: Vypiš sezónu a měsíc, kdy hráči dostali celkově nejméně červených karet. --
--Výpis ak chceme všetky min hodnoty:--
 --2017 February 5
 --2018 October 5

SELECT Season.name as season, Months.name as month, SUM(red_cards) as Cards
FROM PlayerStatistics JOIN Season
ON PlayerStatistics.id_season=Season.id JOIN Months
ON PlayerStatistics.id_month=Months.id
GROUP by Months.name, Season.name
HAVING SUM(red_cards) <= ALL
(
 SELECT SUM(red_cards)
 FROM PlayerStatistics JOIN Season
 ON PlayerStatistics.id_season=Season.id JOIN Months
 ON PlayerStatistics.id_month=Months.id
 GROUP by Months.name, Season.name
)
--Uloha D: Vypiš sezónu a měsíc, kdy hráči dostali celkově nejméně červených karet.--
--Výpis ak chceme iba jednu min hodnotu:
 --2017 February 5
SELECT TOP 1 S.name as season, M.name as month, SUM(red_cards) as cards
from PlayerStatistics P, Season S, Months M
where P.id_month=M.id and P.id_season=S.id
GROUP by M.name, S.name
order by cards asc
