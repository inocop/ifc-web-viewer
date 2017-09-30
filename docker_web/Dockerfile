FROM centos:7.3.1611

MAINTAINER inocop


WORKDIR /tmp


# useradd dotnetdev
RUN useradd dotnetdev && echo "dotnetdev:password" | chpasswd
RUN mkdir -p /opt/dotnet
RUN chown dotnetdev:dotnetdev /opt/dotnet
RUN chmod 2775 /opt/dotnet


# install yum-plugin-fastestmirror
RUN yum install -y yum-plugin-fastestmirror which git unzip
RUN echo "include_only=.jp" >>  /etc/yum/pluginconf.d/fastestmirror.conf


# apache install
RUN yum install -y httpd mod_ssl mod_rewrite mod_proxy
RUN systemctl enable httpd.service
COPY ./dotnet.conf /etc/httpd/conf.d/dotnet.conf


# install dotnet
RUN rpm --import https://packages.microsoft.com/keys/microsoft.asc
RUN sh -c 'echo -e "[packages-microsoft-com-prod]\nname=packages-microsoft-com-prod \nbaseurl=https://packages.microsoft.com/yumrepos/microsoft-rhel7.3-prod\nenabled=1\ngpgcheck=1\ngpgkey=https://packages.microsoft.com/keys/microsoft.asc" > /etc/yum.repos.d/dotnetdev.repo'
RUN yum install -y libunwind libicu dotnet-sdk-2.0.2
COPY ./dotnet.service /etc/systemd/system/dotnet.service
RUN systemctl enable dotnet.service


# install debugger
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /opt/dotnet/vsdbg


# enable sshd for debugg
RUN yum -y install openssh-server
RUN systemctl enable sshd.service


# exec
CMD ["/sbin/init"]
