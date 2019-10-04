-- spAddProdotto
INSERT INTO tblProdotto ( IDcategoria, Nome, Descrizione, Foto, Prezzo, Disponibilit� )
VALUES ([@categoria], [@nome], [@descrizione], [@foto], [@prezzo], [@disponibilita]);

-- spAddUtente
INSERT INTO tblUtente ( UserName, [Password], Nome, Indirizzo, Telefono, EMail, Ruolo )
VALUES ([@UserName], [@Password], [@Nome], [@Indirizzo], [@Telefono], [@EMail], 2);

-- spAggiornaCarrello
UPDATE tblCarrello SET Quantit� = [@quantita]
WHERE IDutente=[@utente] AND IDprodotto=[@prodotto];

-- spAggiornaCategoria
UPDATE tblCategoria SET Nome = [@nome], Descrizione = [@descrizione], Foto = [@foto]
WHERE IDcategoria=[@categoria];

-- spAggiornaOrdine
SELECT [IDutente], [IDprodotto], [Quantit�]
FROM tblOrdine;

-- spAggiungiCarrello
INSERT INTO tblCarrello ( IDutente, IDprodotto, Quantit� )
VALUES ([@IDutente], [@IDprodotto], [@Quantita]);

-- spAggiungiCategoria
INSERT INTO tblCategoria ( Nome, Descrizione, Foto )
VALUES ([@nome], [@descrizione], [@foto]);

-- spAggiungiOrdine
SELECT *
FROM tblOrdine
WHERE IDutente=0;

-- spEditProdotto
UPDATE tblProdotto SET IDcategoria = [@categoria], Nome = [@nome], Descrizione = [@descrizione], Foto = [@foto], Prezzo = [@prezzo], Disponibilit� = [@disponibilita]
WHERE IDprodotto=[@IDprodotto];

-- spEditUtente
UPDATE tblUtente SET UserName = [@UserName], [Password] = [@Password], Ruolo = [@Ruolo]
WHERE IDutente=[@IDutente];

-- spEliminaCarrello
DELETE *
FROM tblCarrello
WHERE IDutente=[@utente] AND IDprodotto=[@prodotto];

-- spEliminaCategoria
DELETE [IDcategoria]
FROM tblCategoria
WHERE IDcategoria=[@categoria];

-- spEliminaInteroCarrello
DELETE *
FROM tblCarrello
WHERE IDutente=[@utente];

-- spEliminaOrdine
DELETE *
FROM tblOrdine
WHERE IDutente=[@utente];

-- spEliminaProdotto
DELETE *
FROM tblProdotto
WHERE IDprodotto=[@prodotto];

-- spEliminaUtente
DELETE *
FROM tblUtente
WHERE IDutente=[@utente];

-- spLoginAdmin
SELECT tblUtente.IDutente, tblRuolo.Ruolo
FROM tblRuolo INNER JOIN tblUtente ON tblRuolo.IDruolo = tblUtente.Ruolo
WHERE UserName=[@UserName] AND Password=[@Password];

-- spLoginUtente
SELECT [IDutente]
FROM tblUtente
WHERE UserName=[@UserName] And Password=[@Password];

-- spParzialiCarrello
SELECT [IDutente], [Quantit�], [Prezzo], [Prezzo]*[Quantit�] AS Parziale
FROM tblProdotto INNER JOIN tblCarrello ON [tblProdotto].[IDprodotto]=[tblCarrello].[IDprodotto];

-- spParzialiOrdine
SELECT [IDutente], [Quantit�], [Prezzo], [Prezzo]*[Quantit�] AS Parziale
FROM tblProdotto INNER JOIN tblOrdine ON [tblProdotto].[IDprodotto]=[tblOrdine].[IDprodotto];

-- spProdottoDettagli
SELECT *
FROM tblProdotto
WHERE IDprodotto=[@prodotto];

-- spProdottoPerCategoria
SELECT *
FROM tblProdotto
WHERE IDcategoria=[@categoria];

-- spProdottoRicerca
SELECT IDprodotto, tblProdotto.Nome AS Prodotto, tblCategoria.Nome AS Categoria, Prezzo, Disponibilit�, tblProdotto.Foto
FROM tblCategoria INNER JOIN tblProdotto ON tblCategoria.IDcategoria = tblProdotto.IDcategoria
WHERE (((tblProdotto.Nome) Like [@prodotto]));

-- spRuolo
SELECT *
FROM tblRuolo
ORDER BY Ruolo;

-- spTotaliCarrello
SELECT IDutente, Sum(Quantit�) AS TotaleQuantit�, Sum(Parziale) AS TotaleSpesa
FROM spParzialiCarrello
GROUP BY IDutente
HAVING IDutente=[@utente];

-- spTotaliOrdine
SELECT IDutente, Sum(Quantit�) AS TotaleQuantit�, Sum(Parziale) AS TotaleSpesa
FROM spParzialiOrdine
GROUP BY IDutente
HAVING IDutente=[@utente];

-- spVerificaProdotto
SELECT Nome
FROM tblProdotto
WHERE Nome=[@nomeProdotto];

-- spVerificaUtente
SELECT UserName
FROM tblUtente
WHERE UserName=[@nomeUtente];

-- spVisualizzaCarrello
SELECT [tblCarrello].[IDutente], [tblCarrello].[IDprodotto], [tblCarrello].[Quantit�], [tblProdotto].[Nome], [tblProdotto].[Descrizione], [tblProdotto].[Foto], [tblProdotto].[Prezzo]
FROM tblProdotto INNER JOIN tblCarrello ON [tblProdotto].[IDprodotto]=[tblCarrello].[IDprodotto]
WHERE [tblCarrello].[IDutente]=[@utente]
ORDER BY [tblProdotto].[Nome];

-- spVisualizzaCategoria
SELECT *
FROM tblCategoria
ORDER BY Nome;

-- spVisualizzaOrdine
SELECT tblOrdine.IDutente, tblProdotto.IDprodotto, tblOrdine.Quantit�, tblProdotto.Nome, tblProdotto.Descrizione, tblProdotto.Foto, tblProdotto.Prezzo
FROM tblProdotto INNER JOIN tblOrdine ON tblProdotto.IDprodotto = tblOrdine.IDprodotto
WHERE (((tblOrdine.IDutente)=[@utente]))
ORDER BY tblProdotto.Nome;

-- spVisualizzaUtente
SELECT *
FROM tblUtente
ORDER BY UserName;
