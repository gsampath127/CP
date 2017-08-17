<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" version="1.0" exclude-result-prefixes="msxsl">
  <xsl:template match="/">
    <ul id="NavMenu">      
        <xsl:for-each select="menuRoot/menuItem">
          <xsl:if test="@type=''link''">
            <li class="top">
              <xsl:attribute name="class">
                <xsl:value-of select="@cssClass" />
              </xsl:attribute>
              <xsl:attribute name="id">
                <xsl:value-of select="@id" />
              </xsl:attribute>              
                <a>                
                  <xsl:attribute name="title">
                    <xsl:value-of select="@toolTip" />
                  </xsl:attribute>
                  <xsl:attribute name="onclick">
                    <xsl:value-of select="@onclick" />
                  </xsl:attribute>
                  <xsl:attribute name="href">
                    <xsl:value-of select="@href" />
                  </xsl:attribute>
                  <xsl:attribute name="target">
                    <xsl:value-of select="@target" />
                  </xsl:attribute>
                  <div class="divMenuTD">
                    <xsl:value-of select="@displayName" disable-output-escaping="yes"/>
                  </div>
                </a>
            </li>
          </xsl:if>
          <xsl:if test="@type=''menuDropDown''">
            <li class="top">
              <xsl:attribute name="class">
                <xsl:value-of select="@cssClass" />
              </xsl:attribute>              
                <a>
                  <div class="divMenuTD">
                    <xsl:value-of select="@displayName" disable-output-escaping="yes"/>
                  </div>
                </a>              
              <ul>
                <xsl:for-each select="menuItem">
                  <li>
                    <xsl:attribute name="class">
                      <xsl:value-of select="@cssClass" />
                    </xsl:attribute>
                    <xsl:attribute name="id">
                      <xsl:value-of select="@id" />
                    </xsl:attribute>
                    <a>                      
                      <xsl:attribute name="title">
                        <xsl:value-of select="@toolTip" />
                      </xsl:attribute>
                      <xsl:attribute name="onclick">
                        <xsl:value-of select="@onclick" />
                      </xsl:attribute>
                      <xsl:attribute name="href">
                        <xsl:value-of select="@href" />
                      </xsl:attribute>
                      <xsl:attribute name="target">
                        <xsl:value-of select="@target" />
                      </xsl:attribute>                      
                      <xsl:value-of select="@displayName" disable-output-escaping="yes"/>
                    </a>
                  </li>
                </xsl:for-each>
              </ul>
            </li>
          </xsl:if>
        </xsl:for-each>      
    </ul>
  </xsl:template>
</xsl:stylesheet>