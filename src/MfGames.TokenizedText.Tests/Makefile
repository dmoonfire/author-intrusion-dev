CSHARP_SOURCE = $(wildcard *.cs)
DLL = MfGames.TokenizedText.Tests.dll

all: ../../bin/$(DLL)

../../bin/$(DLL): $(CSHARP_SOURCE) ../../bin/MfGames.TokenizedText.dll
	if [ ! -d ../../bin ];then mkdir ../../bin; chmod 0755 ../../bin; fi
	gmcs -target:library \
		-out:../../bin/$(DLL) \
		-r:../../packages/NUnit.2.6.3/lib/nunit.framework.dll \
		$(CSHARP_SOURCE)
	chmod 0644 ../../bin/$(DLL)
