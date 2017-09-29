FROM centos:7.3.1611

MAINTAINER inocop


# install yum-plugin-fastestmirror
RUN yum install -y yum-plugin-fastestmirror
RUN echo "include_only=.jp" >>  /etc/yum/pluginconf.d/fastestmirror.conf


# apache install
RUN yum install -y httpd mod_ssl
RUN systemctl enable httpd.service
COPY ./dotnet.conf /etc/httpd/conf.d/dotnet.conf


# add db tools
RUN yum install -y wget unzip php php-mysqli php-mbstring
RUN mkdir -p /var/www/html/tools
RUN cd /var/www/html/tools && wget https://github.com/vrana/adminer/releases/download/v4.3.1/adminer-4.3.1.php
RUN mv /var/www/html/tools/adminer-4.3.1.php /var/www/html/tools/adminer.php


RUN mkdir -p /opt/IfcOpenShell
WORKDIR /opt/IfcOpenShell
RUN wget http://sourceforge.net/projects/ifcopenshell/files/0.4.0-rc2/IfcConvert-0.4.0-rc2-linux64.zip/download -O IfcConvert.zip
RUN unzip IfcConvert.zip
RUN ln -s /opt/IfcOpenShell/IfcConvert/IfcConvert /usr/local/bin

# install dotnet
#RUN yum -y install libunwind libicu
#RUN curl -sSL -o dotnet.tar.gz https://aka.ms/dotnet-sdk-2.0.0-linux-x64
#RUN mkdir -p /opt/dotnet/sdk2.0.0 
#RUN tar zxf dotnet.tar.gz -C /opt/dotnet/sdk2.0.0 
#RUN ln -s /opt/dotnet/sdk2.0.0/dotnet /usr/local/bin

# exec
CMD ["/sbin/init"]