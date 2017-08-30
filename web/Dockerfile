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
RUN yum install -y wget php php-mysqli php-mbstring 
RUN mkdir -p /var/www/html/tools
RUN cd /var/www/html/tools && wget https://github.com/vrana/adminer/releases/download/v4.3.1/adminer-4.3.1.php
RUN mv /var/www/html/tools/adminer-4.3.1.php /var/www/html/tools/adminer.php 


WORKDIR /tmp

# add ifcopenshell
RUN yum install -y git \
                   cmake \
                   gcc \
                   gcc-c++ \
                   boost-devel \
                   libicu-devel \
                   tbb-devel \
                   ftgl-devel \
                   mesa-libGL-devel \
                   mesa-libGLU-devel \
                   freetype-devel \
                   swig \
                   python-devel \
                   OCE-devel

RUN cd /opt && git clone https://github.com/IfcOpenShell/IfcOpenShell.git
#RUN mkdir -p /opt/IfcOpenShell/build
#RUN cd /opt/IfcOpenShell/build && cmake ../cmake -DCOLLADA_SUPPORT=0
#RUN cd /opt/IfcOpenShell/build make -j


# install dotnet
#RUN yum -y install libunwind libicu
#RUN curl -sSL -o dotnet.tar.gz https://aka.ms/dotnet-sdk-2.0.0-linux-x64
#RUN mkdir -p /opt/dotnet/sdk2.0.0 
#RUN tar zxf dotnet.tar.gz -C /opt/dotnet/sdk2.0.0 
#RUN ln -s /opt/dotnet/sdk2.0.0/dotnet /usr/local/bin

# exec
CMD ["/sbin/init"]