CSHARP_SOURCE = \
	$(wildcard *.cs) \
	$(wildcard Commands/*.cs) \
	$(wildcard Changes/*.cs)
DLL = MfGames.TokenizedText.dll

all: ../../bin/$(DLL)

../../bin/$(DLL): $(CSHARP_SOURCE)
	if [ ! -d ../../bin ];then mkdir ../../bin; chmod 0755 ../../bin; fi
	gmcs -target:library \
		-out:../../bin/$(DLL) \
		$(CSHARP_SOURCE)
	chmod 0644 ../../bin/$(DLL)
