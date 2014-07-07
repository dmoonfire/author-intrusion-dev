all:
	$(MAKE) -C src

clean:
	rm -rf bin

restore:
	nuget restore packages/packages.config -PackagesDirectory packages
