all: build test

build:
	$(MAKE) -C src

clean:
	rm -rf bin

test:
	cp packages/NUnit.2.6.3/lib/nunit.framework.dll bin
	cp packages/Humanizer.1.28.0/lib/portable-win+net40+sl50+wp8+wpa81/Humanizer.dll bin
	nunit-console -framework=4.0 bin/*.Tests.dll

restore:
	nuget restore packages/packages.config -PackagesDirectory packages
