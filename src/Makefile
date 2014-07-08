all:
	$(MAKE) -C MfGames.TokenizedText
	$(MAKE) -C MfGames.TokenizedText.Tests

format:
	narrange . -c:narrange.xml
	./call_Uncrustify.sh . cs
